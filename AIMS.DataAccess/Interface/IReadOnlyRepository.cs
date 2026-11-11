using System.Linq.Expressions;
namespace AIMS.DataAccess.Interface
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetPageAsync(int skip, int take, CancellationToken cancellationToken = default);
        Task<TEntity?> GetByIdAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        
    }
}
