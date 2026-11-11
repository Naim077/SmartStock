using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace AIMS.DataAccess.Implementation
{
    public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        public Repository(ApplicationDbContext dbContext) : base(dbContext)
        {
            if (typeof(TEntity) != typeof(Category) && typeof(TEntity) != typeof(Product)
                && typeof(TEntity) != typeof(Supplier) && typeof(TEntity) != typeof(Customer))
                throw new InvalidOperationException("Generic writes are restricted to catalogue entities. Use an explicit atomic workflow for posting, stock, accounts, Identity, settings and history.");
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);
            EnsureCleanUnitOfWork();
            foreach (var key in KeyProperties.Where(property => property.ValueGenerated == ValueGenerated.OnAdd))
            {
                var value = key.PropertyInfo!.GetValue(entity);
                if (!Equals(value, Activator.CreateInstance(key.ClrType)))
                    throw new ArgumentException("Do not supply a database-generated identifier when adding a record.", nameof(entity));
            }

            // Copy scalars, not a caller-supplied navigation graph containing unrelated entities.
            var stored = Activator.CreateInstance<TEntity>();
            var entry = DbContext.Entry(stored);
            CopyWritableScalars(entity, entry, adding: true);
            entry.State = EntityState.Added;
            try
            {
                await DbContext.SaveChangesAsync(cancellationToken);
                CopyPersistedScalars(entry, entity);
            }
            catch
            {
                entry.State = EntityState.Detached;
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);
            EnsureCleanUnitOfWork();
            var keys = KeyProperties.Select(property => property.PropertyInfo!.GetValue(entity)!).ToArray();
            ValidateKeys(keys);
            var stored = await DbSet.FindAsync(keys, cancellationToken);
            if (stored is null) return false;
            var entry = DbContext.Entry(stored);
            var token = EntityType.FindProperty("RowVersion")?.PropertyInfo?.GetValue(entity) as byte[];
            SetConcurrencyToken(entry, token);
            CopyWritableScalars(entity, entry, adding: false);
            try
            {
                await DbContext.SaveChangesAsync(cancellationToken);
                CopyPersistedScalars(entry, entity);
                return true;
            }
            catch
            {
                // A failed operation must not be accidentally saved by the next repository call.
                entry.State = EntityState.Detached;
                throw;
            }
        }

        public async Task<bool> DeleteAsync(object[] keyValues, byte[] rowVersion, CancellationToken cancellationToken = default)
        {
            ValidateKeys(keyValues);
            EnsureCleanUnitOfWork();
            var stored = await DbSet.FindAsync(keyValues, cancellationToken);
            if (stored is null) return false;
            var entry = DbContext.Entry(stored);
            SetConcurrencyToken(entry, rowVersion);
            entry.State = EntityState.Deleted;
            try
            {
                await DbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch
            {
                entry.State = EntityState.Detached;
                throw;
            }
        }

        private void EnsureCleanUnitOfWork()
        {
            if (DbContext.ChangeTracker.HasChanges())
                throw new InvalidOperationException("Save or discard pending changes before using single-record repository CRUD. It must not commit another workflow's changes.");
        }

        private void SetConcurrencyToken(EntityEntry<TEntity> entry, byte[]? supplied)
        {
            var property = EntityType.FindProperty("RowVersion");
            if (property is null) return;
            if (supplied is not { Length: 8 })
                throw new ArgumentException("Supply the eight-byte RowVersion returned when the record was read.", nameof(supplied));
            if (entry.Property(property.Name).CurrentValue is byte[] current && !current.SequenceEqual(supplied))
                throw new DbUpdateConcurrencyException("The record changed after it was read. Reload it before retrying.");
            entry.Property(property.Name).OriginalValue = supplied.ToArray();
        }

        private void CopyWritableScalars(TEntity source, EntityEntry<TEntity> target, bool adding)
        {
            foreach (var property in EntityType.GetProperties())
            {
                if (property.ValueGenerated == ValueGenerated.OnAddOrUpdate || property.PropertyInfo is null) continue;
                if (property.IsKey() && (!adding || property.ValueGenerated == ValueGenerated.OnAdd)) continue;
                var value = property.PropertyInfo.GetValue(source);
                target.Property(property.Name).CurrentValue = value is byte[] bytes ? bytes.ToArray() : value;
            }
        }

        private void CopyPersistedScalars(EntityEntry<TEntity> source, TEntity target)
        {
            foreach (var property in EntityType.GetProperties())
            {
                if (property.PropertyInfo?.SetMethod is null) continue;
                var value = source.Property(property.Name).CurrentValue;
                property.PropertyInfo.SetValue(target, value is byte[] bytes ? bytes.ToArray() : value);
            }
        }
    }
}
