using CaseFlow.BLL.Dto.Expense;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveExpenseService
{
    Task<Expense> CreateExpenseAsync(int caseId, CreateExpenseDto dto, int detectiveId);
    Task<Expense> UpdateExpenseAsync(UpdateExpenseDto dto, int detectiveId);
    Task DeleteExpenseAsync(int expenseId, int detectiveId);
    
    Task<List<Expense>> GetExpensesAsync(int detectiveId);
    Task<List<Expense>> GetDeclinedExpensesAsync(int detectiveId);
    Task<List<Expense>> GetAcceptedExpensesAsync(int detectiveId);
    Task<Expense?> GetExpenseAsync(int expenseId, int detectiveId);
}