using System.Linq.Expressions;

namespace NfeFiscal.Repository.Base;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Find(Expression<Func<T, bool>> predicate);
    Task<T?> FindById(int id);
    Task Add(T entity);
    Task AddRange(IEnumerable<T> entities);
    Task Update(T entity);
    Task Delete(T entity);
    Task<bool> Any(Expression<Func<T, bool>> predicate);
    Task<ICollection<T>> FindMany(Expression<Func<T, bool>> predicate);
}
