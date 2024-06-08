using Fintrellis.Models;


namespace Fintrellis.Repositories.Common;

/// <summary>
/// Interface for an Abstract Repository.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> : IDisposable
    where TEntity : AbstractEntity
{
    /// <summary>
    /// Gets all <see cref="TEntity"/>.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAll();

    /// <summary>
    /// Gets a single <see cref="TEntity"/> by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetById(int id);

    /// <summary>
    /// Adds a new <see cref="TEntity"/> instance.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> Add(TEntity entity);

    /// <summary>
    /// Updates an existing <see cref="TEntity"/> instance.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Deletes an existing <see cref="TEntity"/> by <see cref="TEntity.Id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(int id);
}

