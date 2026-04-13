using Marketplace.Domain;
using Marketplace.Framework;
using Commands = Marketplace.Contracts.ClassifiedAdds.V1;

namespace Marketplace.Api;

public class ClassifiedAdApplicationService
{
    private readonly IEntityStore _store;

    public ClassifiedAdApplicationService(IEntityStore store)
    {
        _store = store;
    }

    public Task Handle(Commands.Create command)
    {
        var classifiedAd = new ClassifiedAd(
            new ClassifiedAdId(command.Id),
            new UserId(command.OwnerId));
        return _store.Save(classifiedAd);
    }
}