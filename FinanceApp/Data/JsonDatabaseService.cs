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

        // Retrieve the list of expenses from the JSON file
        public List<Expense> GetExpenses()
        {
            if (!File.Exists(_filePath)) return new List<Expense>();

            var json = File.ReadAllText(_filePath);
            var jsonObj = JsonConvert.DeserializeObject<DatabaseFile>(json);
            return jsonObj?.ExpensesTable?.Rows ?? new List<Expense>();
        }

        // Add an expense to the JSON file
        public void AddExpense(Expense expense)
        {
            var data = new DatabaseFile();

            // If the file exists, load its content
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                data = JsonConvert.DeserializeObject<DatabaseFile>(json) ?? new DatabaseFile();
            }

            // Get the current maximum id from the existing rows, if any
            var nextId = data.ExpensesTable.Rows.Any()
                ? data.ExpensesTable.Rows.Max(e => e.Id) + 1
                : 1;  // If no expenses exist, start with id = 1

            // Assign the new expense the next available id
            expense.Id = nextId;

            // Add the new expense to the rows
            data.ExpensesTable.Rows.Add(expense);

            // Serialize the updated data back to the JSON file
            var updatedJson = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, updatedJson);
        }
    }

    // Represents the structure of the JSON file
    public class DatabaseFile
    {
        [JsonProperty("expensesTable")]
        public ExpensesTable ExpensesTable { get; set; } = new ExpensesTable();
    }

    public class ExpensesTable
    {
        [JsonProperty("columns")]
        public List<Column> Columns { get; set; } = new List<Column>();

        [JsonProperty("rows")]
        public List<Expense> Rows { get; set; } = new List<Expense>();
    }

    public class Column
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool PrimaryKey { get; set; }
        public bool AutoIncrement { get; set; }
    }
}
