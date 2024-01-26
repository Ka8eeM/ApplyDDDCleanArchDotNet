namespace Project.Application.Messaging;

public interface ICachedQuery
{
   public string CacheKey { get; }
    public TimeSpan? Expiration { get; }
}

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery
{

}