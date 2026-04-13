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
 
 [HttpPut("name")]
 public async Task<IActionResult> Put(Commands.SetaTitle command)
 {
  await _applicationService.Handle(command);
  return NoContent();
 }
 
 [HttpPut("text")]
 public async Task<IActionResult> Put(Commands.UpdateText command)
 {
  await _applicationService.Handle(command);
  return NoContent();
 }
 
 [HttpPut("price")]
 public async Task<IActionResult> Put(Commands.UpdatePrice command)
 {
  await _applicationService.Handle(command);
  return NoContent();
 }
 
 [HttpPut("publish")]
 public async Task<IActionResult> Put(Commands.RequestToPublish command)
 {
  await _applicationService.Handle(command);
  return NoContent();
 }
 
}