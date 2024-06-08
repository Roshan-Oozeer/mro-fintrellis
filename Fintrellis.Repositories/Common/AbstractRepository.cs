using Fintrellis.Models;

using Microsoft.Extensions.Logging;


namespace Fintrellis.Repositories.Common;

public abstract class AbstractRepository<TEntity> : IRepository<TEntity>
	where TEntity : AbstractEntity
{
    private readonly DataContext _dataContext;
    private readonly ILogger _logger;
    private bool _disposed = false;

    /// <summary>
    /// Creates an instance of child repository from <see cref="AbstractRepository{TEntity}"/>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    protected AbstractRepository(DataContext context, ILogger logger)
	{
        _dataContext = context;
        _logger = logger;
	}

    ~AbstractRepository()
    {
        Dispose(false);
    }

    protected DataContext DbContext => _dataContext;

    protected ILogger Logger => _logger;


    #region IAbstractRepository Implementation

    // <summary>
    /// Gets all <see cref="TEntity"/>.
    /// </summary>
    /// <returns></returns>
    public abstract Task<IEnumerable<TEntity>> GetAll();

    /// <summary>
    /// Gets a single <see cref="TEntity"/> by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public abstract Task<TEntity> GetById(int id);

    /// <summary>
    /// Adds a new <see cref="TEntity"/> instance.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract Task<TEntity> Add(TEntity entity);

    /// <summary>
    /// Updates an existing <see cref="TEntity"/> instance.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Deletes an existing <see cref="TEntity"/> by <see cref="TEntity.Id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public abstract Task<bool> Delete(int id);

    #endregion


    #region IDisposable Implementation

    public void Dispose()
    {
        // Dispose of unmanaged resources.
        Dispose(true);

        // Suppress finalization.
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            // TODO: dispose managed state (managed objects).
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        _disposed = true;
    }

    #endregion
}

