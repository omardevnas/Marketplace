using Marketplace.Domain;
using Marketplace.Framework;
using Commands = Marketplace.Contracts.ClassifiedAdds.V1;

namespace Marketplace.Api;

public class ClassifiedAdApplicationService:IApplicationService
{
    private readonly IClassifiedAdRepository _repository;
    private readonly ICurrencyLookup _currencyLookUp;

    public ClassifiedAdApplicationService(IEntityStore repository, ICurrencyLookup currencyLookUp)
    {
        _repository = repository;
        _currencyLookUp = currencyLookUp;
    }
    
    public Task Handle(object command) =>
        command switch
        {
            Commands.Create cmd =>
                HandleCreate(cmd),
            Commands.SetaTitle cmd =>
                HandleUpdate(
                    cmd.Id,
                    c => c.SetTitle(
                        ClassifiedAdTitle.FromString(cmd.Title)
                    )
                ),
            Commands.UpdateText cmd =>
                HandleUpdate(
                    cmd.Id,
                    c => c.UpdateText(
                        ClassifiedAdText.FromString(cmd.Text)
                    )
                ),
            Commands.UpdatePrice cmd =>
                HandleUpdate(
                    cmd.Id,
                    c => c.UpdatePrice(
                        Price.FromDecimal(
                            cmd.Price,
                            cmd.Currency,
                            _currencyLookUp
                        )
                    )
                ),
            Commands.RequestToPublish cmd =>
                HandleUpdate(
                    cmd.Id,
                    c => c.RequestToPublish()
                ),
            _ => Task.CompletedTask
        };
    private async Task HandleCreate(Commands.Create cmd)
    {
        if (await _repository.Exists(cmd.Id.ToString()))
            throw new InvalidOperationException(
                $"Entity with id {cmd.Id} already exists");
        var classifiedAd = new ClassifiedAd(
            new ClassifiedAdId(cmd.Id),
            new UserId(cmd.OwnerId)
        );
        await _repository.Save(classifiedAd);
    }
    private async Task HandleUpdate(
        Guid classifiedAdId,
        Action<ClassifiedAd> operation
    )
    {
        var classifiedAd = await _repository.Load(
            classifiedAdId.ToString()
        );
        if (classifiedAd == null)
            throw new InvalidOperationException(
                $"Entity with id {classifiedAdId} cannot be found"
            );
        operation(classifiedAd);
        await _repository.Save(classifiedAd);
    }

}