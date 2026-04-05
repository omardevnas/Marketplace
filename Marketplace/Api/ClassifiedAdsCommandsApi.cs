using Microsoft.AspNetCore.Mvc;
using Commands = Marketplace.Contracts.ClassifiedAdds.V1;
namespace Marketplace.Api;

[Route("/ads")]
public class ClassifiedAdsCommandsApi:Controller
{
 [HttpPost]
 public async Task<IActionResult> Post(Commands.Create command)
 {
  return Ok();
 }
 
}