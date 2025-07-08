using CaseFlow.BLL.Dto.Expense;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveExpenseService
{
    Task<ExpenseDto> CreateExpenseAsync(int caseId, CreateExpenseDto dto, int detectiveId);
    Task<ExpenseDto> UpdateExpenseAsync(int expenseId, UpdateExpenseDto dto, int detectiveId);
    Task DeleteExpenseAsync(int expenseId, int detectiveId);
    
    Task<List<ExpenseDto>> GetExpensesAsync(int detectiveId);
    Task<List<ExpenseDto>> GetDeclinedExpensesAsync(int detectiveId);
    Task<List<ExpenseDto>> GetApprovedExpensesAsync(int detectiveId);
    Task<ExpenseDto?> GetExpenseAsync(int expenseId, int detectiveId);
}