using Microsoft.EntityFrameworkCore;
using TechTask.Dal.Entites;

namespace TechTask.Dal.Repositories.Interfaces;

public interface ICrudRepository<TEntity> where TEntity : BaseEntity
{
    DbSet<TEntity> Query();
    Task Create(TEntity entity);
    Task<TEntity?> GetById(Guid? id);
    Task<List<TEntity>> GetAll();
    Task Update(TEntity entity);
    Task Delete(Guid id);
}