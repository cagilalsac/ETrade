﻿using CORE.APP.Domain;
using Microsoft.EntityFrameworkCore;

namespace CORE.APP.Services
{
    /// <summary>
    /// Provides a generic base repository service for handling CRUD operations on entities.
    /// This abstract class defines common database actions such as query, create, update, and delete,
    /// and is designed to work with a specific <typeparamref name="TEntity"/> type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type the service operates on. Must inherit from <see cref="Entity"/> and have a parameterless constructor.</typeparam>
    public abstract class Service<TEntity> : ServiceBase, IDisposable where TEntity : Entity, new()
    {
        /// <summary>
        /// The database context used for data access operations.
        /// </summary>
        private readonly DbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service{TEntity}"/> class.
        /// </summary>
        /// <param name="db">The database context to be used for entity operations.</param>
        protected Service(DbContext db)
        {
            _db = db;
        }



        // *** Synchronous Repository Operations ***

        /// <summary>
        /// Provides a queryable interface to the <typeparamref name="TEntity"/> dataset.
        /// </summary>
        /// <param name="isNoTracking">
        /// If true, disables entity tracking for read-only scenarios, improving performance.
        /// If false, enables tracking for updates.
        /// </param>
        /// <returns>An <see cref="IQueryable{TEntity}"/> representing the dataset.</returns>
        protected virtual IQueryable<TEntity> Query(bool isNoTracking = true)
        {
            return isNoTracking ? _db.Set<TEntity>().AsNoTracking() : _db.Set<TEntity>();
        }

        /// <summary>
        /// Persists changes to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        // Way 1:
        //protected virtual int Save()
        //{
        //    return _db.SaveChanges();
        //}
        // Way 2:
        protected virtual int Save() => _db.SaveChanges();

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <param name="save">If true, saves changes immediately; otherwise, defers saving.</param>
        protected void Create(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Add(entity);
            if (save)
                Save();
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="save">If true, saves changes immediately; otherwise, defers saving.</param>
        protected void Update(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Update(entity);
            if (save)
                Save();
        }

        /// <summary>
        /// Removes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <param name="save">If true, saves changes immediately; otherwise, defers saving.</param>
        protected void Delete(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Remove(entity);
            if (save)
                Save();
        }

        // *** ***



        // *** Asynchronous Repository Operations ***

        /// <summary>
        /// Asynchronously saves all pending changes in the current <see cref="DbContext"/> to the database.
        /// </summary>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests, allowing the operation to be cancelled early.
        /// </param>
        /// <returns>The number of state entries written to the database.</returns>
        protected virtual async Task<int> Save(CancellationToken cancellationToken) => await _db.SaveChangesAsync(cancellationToken);

        /// <summary>
        /// Asynchronously adds a new entity to the database context and optionally saves the change.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <param name="save">
        /// If <c>true</c>, the method immediately calls <see cref="Save"/> to persist changes.
        /// If <c>false</c>, saving is deferred for batch operations.
        /// </param>
        protected async Task Create(TEntity entity, CancellationToken cancellationToken, bool save = true)
        {
            _db.Set<TEntity>().Add(entity);
            if (save)
                await Save(cancellationToken);
        }

        /// <summary>
        /// Asynchronously updates an existing entity in the database context and optionally saves the change.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <param name="save">
        /// If <c>true</c>, the method immediately calls <see cref="Save"/> to persist changes.
        /// If <c>false</c>, saving is deferred for batch operations.
        /// </param>
        protected async Task Update(TEntity entity, CancellationToken cancellationToken, bool save = true)
        {
            _db.Set<TEntity>().Update(entity);
            if (save)
                await Save(cancellationToken);
        }

        /// <summary>
        /// Asynchronously removes an entity from the database context and optionally saves the change.
        /// </summary>
        /// <param name="entity">The entity to be removed.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <param name="save">
        /// If <c>true</c>, the method immediately calls <see cref="Save"/> to persist changes.
        /// If <c>false</c>, saving is deferred for batch operations.
        /// </param>
        protected async Task Delete(TEntity entity, CancellationToken cancellationToken, bool save = true)
        {
            _db.Set<TEntity>().Remove(entity);
            if (save)
                await Save(cancellationToken);
        }

        // *** ***



        /// <summary>
        /// Deletes a list of related entities of type <typeparamref name="TRelationalEntity"/> from the database context.
        /// </summary>
        /// <typeparam name="TRelationalEntity">
        /// The type of the related entity to delete. Must inherit from <see cref="Entity"/> and have a parameterless constructor.
        /// </typeparam>
        /// <param name="relationalEntities">The list of related entities to be removed.</param>
        /// <remarks>
        /// This method does not call <c>SaveChanges()</c>, so you must explicitly save the context after calling this method
        /// if you want changes to persist in the database.
        /// </remarks>
        protected void Delete<TRelationalEntity>(List<TRelationalEntity> relationalEntities) where TRelationalEntity : Entity, new()
        {
            _db.Set<TRelationalEntity>().RemoveRange(relationalEntities);
        }

        /// <summary>
        /// Releases the database context and any unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
