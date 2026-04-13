namespace Marketplace.Api;

public interface IApplicationService
{
    public Task Handle(object command);
}