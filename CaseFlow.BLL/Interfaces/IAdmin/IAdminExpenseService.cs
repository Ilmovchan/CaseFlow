using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IAdmin;

public interface IAdminExpenseService
{
    Task<Expense?> GetExpenseAsync(int expenseId);
    Task<List<Expense>> GetExpensesFromCaseAsync(int caseId);
    Task<List<Expense>> GetExpensesAsync();
    Task<List<Expense>> GetPendingExpensesAsync();
}