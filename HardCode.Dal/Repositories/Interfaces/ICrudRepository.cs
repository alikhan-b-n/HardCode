using HardCode.Dal.Entites;
using Microsoft.EntityFrameworkCore;

namespace HardCode.Dal.Repositories.Interfaces;

public interface ICrudRepository<TEntity> where TEntity : BaseEntity
{
    DbSet<TEntity> Query();
    Task Create(TEntity entity);
    Task CreateMany(List<TEntity> entities);
    Task<TEntity?> GetById(Guid? id);
    Task<List<TEntity>> GetAll();
    Task Update(TEntity entity);
    Task Delete(Guid id);
}