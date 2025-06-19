using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Detective;

public abstract class DetectiveBaseController : ControllerBase
{
    protected int DetectiveId => 1;
}