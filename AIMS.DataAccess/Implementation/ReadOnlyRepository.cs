
using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
namespace AIMS.DataAccess.Implementation
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;
        protected readonly IEntityType EntityType;
        protected readonly IReadOnlyList<IProperty> KeyProperties;

        public ReadOnlyRepository(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext);
            DbContext = dbContext;
            EntityType = dbContext.Model.FindEntityType(typeof(TEntity))
                ?? throw new ArgumentException($"{typeof(TEntity).Name} is not a mapped entity.");
            if (EntityType.GetSchema() != "ss")
                throw new ArgumentException("Business repositories cannot expose framework Identity records.");
            KeyProperties = EntityType.FindPrimaryKey()?.Properties
                ?? throw new ArgumentException("The entity must have a primary key.");
            DbSet = dbContext.Set<TEntity>();
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return OrderedQuery().ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(skip);
            if (take is < 1 or > 200)
                throw new ArgumentOutOfRangeException(nameof(take), "Page size must be between 1 and 200.");
            return OrderedQuery().Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public Task<TEntity?> GetByIdAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            ValidateKeys(keyValues);
            var parameter = Expression.Parameter(typeof(TEntity), "entity");
            Expression? predicate = null;
            for (var index = 0; index < KeyProperties.Count; index++)
            {
                var property = KeyProperties[index];
                var access = Expression.Call(typeof(EF), nameof(EF.Property), [property.ClrType], parameter, Expression.Constant(property.Name));
                var comparison = Expression.Equal(access, Expression.Constant(keyValues[index], property.ClrType));
                predicate = predicate is null ? comparison : Expression.AndAlso(predicate, comparison);
            }
            // Detached reads avoid the duplicate-tracked-instance problem in facade updates.
            return DbSet.AsNoTracking().SingleOrDefaultAsync(
                Expression.Lambda<Func<TEntity, bool>>(predicate!, parameter), cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return DbSet.AsNoTracking().AnyAsync(predicate, cancellationToken);
        }

        protected void ValidateKeys(object[] keyValues)
        {
            ArgumentNullException.ThrowIfNull(keyValues);
            if (keyValues.Length != KeyProperties.Count)
                throw new ArgumentException($"{typeof(TEntity).Name} requires {KeyProperties.Count} key values in model key order.", nameof(keyValues));
            for (var index = 0; index < keyValues.Length; index++)
            {
                if (keyValues[index] is null || !KeyProperties[index].ClrType.IsInstanceOfType(keyValues[index]))
                    throw new ArgumentException($"{KeyProperties[index].Name} must be a {KeyProperties[index].ClrType.Name}.", nameof(keyValues));
                if (keyValues[index] is string text && string.IsNullOrWhiteSpace(text))
                    throw new ArgumentException("String keys cannot be empty.", nameof(keyValues));
                if (keyValues[index] is Guid guid && guid == Guid.Empty)
                    throw new ArgumentException("GUID keys cannot be empty.", nameof(keyValues));
            }
        }

        private IOrderedQueryable<TEntity> OrderedQuery()
        {
            var ordered = DbSet.AsNoTracking().OrderByDescending(entity => EF.Property<object>(entity, KeyProperties[0].Name));
            foreach (var property in KeyProperties.Skip(1))
                ordered = ordered.ThenByDescending(entity => EF.Property<object>(entity, property.Name));
            return ordered;
        }

        Task<bool> IReadOnlyRepository<TEntity>.AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
