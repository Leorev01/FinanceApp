using FinanceApp.Models;
using Newtonsoft.Json;

namespace FinanceApp.Data
{
    public class JsonDatabaseService
    {
        private readonly string _filePath;

        public JsonDatabaseService(string filePath)
        {
            _filePath = filePath;
        }

        public List<Expense> GetExpenses()
        {
            if (!File.Exists(_filePath)) return new List<Expense>();

            var json = File.ReadAllText(_filePath);
            var jsonObj = JsonConvert.DeserializeObject<DatabaseFile>(json);
            return jsonObj?.ExpensesTable?.Rows ?? new List<Expense>();
        }

        public void AddExpense(Expense expense)
        {
            var data = new DatabaseFile();

            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                data = JsonConvert.DeserializeObject<DatabaseFile>(json) ?? new DatabaseFile();
            }

            data.ExpensesTable.Rows.Add(expense);

            var updatedJson = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, updatedJson);
        }
    }

    public class DatabaseFile
    {
        [JsonProperty("expensesTable")]
        public ExpensesTable ExpensesTable { get; set; } = new ExpensesTable();
    }

    public class ExpensesTable
    {
        [JsonProperty("rows")]
        public List<Expense> Rows { get; set; } = new List<Expense>();
    }
}
