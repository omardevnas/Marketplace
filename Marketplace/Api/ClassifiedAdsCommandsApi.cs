using Marketplace.Framework;
using Microsoft.AspNetCore.Mvc;
using Commands = Marketplace.Contracts.ClassifiedAdds.V1;
namespace Marketplace.Api;

[Route("/ads")]
public class ClassifiedAdsCommandsApi:Controller
{
 private readonly ClassifiedAdApplicationService _applicationService;
 public ClassifiedAdsCommandsApi(ClassifiedAdApplicationService applicationService)
 {
  _applicationService = applicationService;
 }
  
 [HttpPost]
 public async Task<IActionResult> Post(Commands.Create command)
 {
  await _applicationService.Handle(command); 
  return Ok();
 }
  
}