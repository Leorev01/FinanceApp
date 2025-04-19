using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly FinanceAppContext _context;

        public ExpensesController(FinanceAppContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var expenses = _context.Expenses;
            return View(expenses);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.AddExpense(expense);
                return RedirectToAction("Index");
                  
            }
            return View(expense);
        }
    }
}
