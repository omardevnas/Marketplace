namespace Marketplace.Framework;

public interface IEntityStore
{
    /// <summary>
    /// Loads an entity by id
    /// </summary>
    public Task<T> Load<T>(string id);
    /// <summary>
    /// Persists an entity
    /// </summary>
    public Task<T> Save<T>(T entity);
    /// <summary>
    /// Check if entity with a given id already exists
    /// <typeparam name="T">Entity type</typeparam>
    public Task<bool> Exists<T>(string entityId);
}