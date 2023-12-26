
using Microsoft.EntityFrameworkCore;

class EntityFrameworkRepository<T> : IRepository<T> where T : class
{
    private readonly TodoContext _context;
    private readonly DbSet<T> _dbSet;
    public EntityFrameworkRepository(TodoContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public Task<List<T>> GetAllAsync()
    {
        return _dbSet.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        var entry = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<T> Update(T entity)
    {
        var entry = _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async void Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}