using CaseFlow.BLL.Interfaces.IDetective;
using CaseFlow.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Detective;

[ApiController]
[Route("detective/case")]
public class DetectiveCaseController(IDetectiveCaseService caseService) : ControllerBase
{
    // [HttpGet]
    // public async Task<Case> GetCases() => 
    //     Ok(await caseService.GetCasesAsync());   
}