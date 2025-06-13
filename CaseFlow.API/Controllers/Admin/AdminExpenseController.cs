using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/expenses")]
public class AdminExpenseController(IAdminExpenseService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetExpenses() =>
        Ok(await service.GetExpensesAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(int id)
    {
        var item = await service.GetExpenseAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("case")]
    public async Task<IActionResult> GetExpensesFromCase(int caseId) =>
        Ok(await service.GetExpensesFromCaseAsync(caseId));
    
    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingExpenses() =>
        Ok(await service.GetPendingExpensesAsync());
}