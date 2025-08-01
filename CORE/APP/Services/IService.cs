﻿using CORE.APP.Models;

namespace CORE.APP.Services
{
    /// <summary>
    /// TO BE USED WITH N-LAYERED ARCHITECTURE!
    /// Represents a generic service interface that defines standard operations 
    /// for querying, creating, updating, and deleting entities in a type-safe and reusable way.
    /// </summary>
    /// <typeparam name="TRequest">
    /// The type of the request model used for input operations (e.g., Create or Update). 
    /// Must inherit from <see cref="Request"/> and have a parameterless constructor.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The type of the response model returned from query operations. 
    /// Must inherit from <see cref="QueryResponse"/> and have a parameterless constructor.
    /// </typeparam>
    public interface IService<TRequest, TResponse> where TRequest : Request, new() where TResponse : QueryResponse, new()
    {
        /// <summary>
        /// Retrieves a list of items from the data source.
        /// </summary>
        /// <returns>
        /// A list of <typeparamref name="TResponse"/> objects representing all records.
        /// </returns>
        public List<TResponse> GetList();

        /// <summary>
        /// Retrieves a single item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item to retrieve.</param>
        /// <returns>
        /// A <typeparamref name="TResponse"/> object representing the specified item.
        /// </returns>
        public TResponse GetItem(int id);

        /// <summary>
        /// Creates a new item in the data source using the provided request data.
        /// </summary>
        /// <param name="request">The request object containing the data for the new entity.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating the result of the creation operation.
        /// </returns>
        public CommandResponse Create(TRequest request);

        /// <summary>
        /// Updates an existing item in the data source using the provided request data.
        /// </summary>
        /// <param name="request">The request object containing updated values for the item.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating the result of the update operation.
        /// </returns>
        public CommandResponse Update(TRequest request);

        /// <summary>
        /// Deletes an existing item from the data source by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item to delete.</param>
        /// <returns>
        /// A <see cref="CommandResponse"/> indicating the result of the deletion operation.
        /// </returns>
        public CommandResponse Delete(int id);
    }
}
