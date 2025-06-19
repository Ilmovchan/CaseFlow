using CaseFlow.BLL.Dto.Expense;
using CaseFlow.BLL.Interfaces.IDetective;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Detective;

[ApiController]
[Route("detective/expenses")]
public class DetectiveExpenseController(IDetectiveExpenseService expenseService) : DetectiveBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExpenses() => 
        Ok(await expenseService.GetExpensesAsync(DetectiveId));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExpense(int id)
    {
        var expense = await expenseService.GetExpenseAsync(id, DetectiveId);

        return expense is null ? NotFound() : Ok(expense);
    }

    [HttpPost("{caseId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostExpense(int caseId, CreateExpenseDto newExpense)
    {
        var expense = await expenseService.CreateExpenseAsync(caseId, newExpense, DetectiveId);
        
        return CreatedAtAction(nameof(GetExpense), new {id = expense.Id}, expense);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutExpense(int id, UpdateExpenseDto updateExpense)
    {
        await expenseService.UpdateExpenseAsync(id, updateExpense, DetectiveId);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        await expenseService.DeleteExpenseAsync(id, DetectiveId);
        
        return NoContent();
    }

    [HttpGet("declined")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeclinedExpenses() =>
        Ok(await expenseService.GetDeclinedExpensesAsync(DetectiveId));    
    
    [HttpGet("approved")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetApprovedExpenses() =>
        Ok(await expenseService.GetApprovedExpensesAsync(DetectiveId));
}