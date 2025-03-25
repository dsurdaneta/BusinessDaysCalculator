using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DsuDev.BusinessDays.DataAccess.Models.Base;

namespace DsuDev.BusinessDays.DataAccess;

/// <summary>
/// Interface for generic Repositories
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepository<TEntity> where TEntity : DbModelBase
{
    /// <summary>
    /// Adds the entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Gets all the entities asynchronously.
    /// </summary>
    /// <returns></returns>
    Task<ICollection<TEntity>> GetAllAsync();

    /// <summary>
    /// Gets the entity by identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(int id);

    /// <summary>
    /// Gets the entities that match with the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    ICollection<TEntity> Get(Func<TEntity, bool> expression);

    /// <summary>
    /// Deletes the entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task<bool> DeleteAsync(TEntity entity);

    /// <summary>
    /// Updates the entity asynchronously.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task<bool> UpdateAsync(int id, TEntity entity);

    /// <summary>
    /// Asynchronously verifies if the entity already exist in the repository.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    Task<bool> AnyAsync(TEntity entity);
}
