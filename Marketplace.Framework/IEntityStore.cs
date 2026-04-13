namespace Marketplace.Framework;

public interface IEntityStore
{
    public Task<T> Load<T>(string id);
    public Task<T> Save<T>(T entity);
}