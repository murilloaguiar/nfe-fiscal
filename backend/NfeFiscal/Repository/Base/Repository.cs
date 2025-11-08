using Microsoft.EntityFrameworkCore;
using NfeFiscal.Context;
using System.Linq.Expressions;

namespace NfeFiscal.Repository.Base;

public class Repository<T> : IRepository<T> where T : class
{
    protected NfeContext _context;
    public Repository(NfeContext context)
    {
        _context = context;
    }
    public async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return _context.Set<T>().AsNoTracking().ToList();
    }

    public async Task<T> Find(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(predicate);
    }

    public async Task<T?> FindById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<T>().Update(entity);
    }

    public async Task<bool> Any(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AnyAsync(predicate);
    }

    public async Task<ICollection<T>> FindMany(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }
}
