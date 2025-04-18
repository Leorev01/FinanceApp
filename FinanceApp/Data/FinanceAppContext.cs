using FinanceApp.Models;

namespace FinanceApp.Data
{
    public class FinanceAppContext
    {
        private readonly JsonDatabaseService _db;

        public FinanceAppContext(JsonDatabaseService db)
        {
            _db = db;
        }

        public List<Expense> Expenses => _db.GetExpenses();

        public void AddExpense(Expense expense)
        {
            _db.AddExpense(expense);
        }
    }
}
