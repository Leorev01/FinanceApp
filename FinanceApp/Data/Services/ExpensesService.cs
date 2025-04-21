
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;



namespace FinanceApp.Data.Services
{
    public class ExpensesService : IExpensesService

    {
        private readonly FinanceAppContext _context;
        public ExpensesService(FinanceAppContext context)
        {
            _context = context;
        }
        public Task Add(Expense expense)
        {
            _context.AddExpense(expense);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Expense>> GetAll()
        {
            var expenses = _context.Expenses;
            return (IEnumerable<Expense>)Task.FromResult(expenses.AsEnumerable());
        }
    }
}

