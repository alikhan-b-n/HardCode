using HardCode.Dal.Entites;
using HardCode.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HardCode.Dal.Repositories;

public class Repository<TEntity> : ICrudRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationContext _applicationContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        _dbSet = _applicationContext.Set<TEntity>();
    }

    public DbSet<TEntity> Query()
    {
        return _dbSet;
    }

    public async Task Create(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task CreateMany(List<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task<TEntity?> GetById(Guid? id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Update(TEntity entity)
    {
        _ = await _dbSet.AnyAsync(x => x.Id == entity.Id)
            ? _applicationContext.Entry(entity).State = EntityState.Modified
            : throw new ArgumentException("Entity does not exist");
        await _applicationContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        _dbSet.Remove(await _dbSet.FirstAsync(x => x.Id == id));
        await _applicationContext.SaveChangesAsync();
    }
    
}