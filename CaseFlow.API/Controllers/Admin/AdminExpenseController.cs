using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/expenses")]
public class AdminExpenseController(IAdminExpenseService service) : AdminBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExpenses() =>
        Ok(await service.GetExpensesAsync());

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExpense(int id)
    {
        var item = await service.GetExpenseAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("case/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExpensesFromCase(int id) =>
        Ok(await service.GetExpensesFromCaseAsync(id));
    
    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPendingExpenses() =>
        Ok(await service.GetPendingExpensesAsync());
}