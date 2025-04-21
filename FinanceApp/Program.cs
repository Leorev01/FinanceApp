using FinanceApp.Data;
using FinanceApp.Data.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<FinanceAppContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));


// Register MVC
builder.Services.AddControllersWithViews();

// Register your JSON-based context
builder.Services.AddSingleton(provider =>
{
    string jsonPath = builder.Configuration.GetValue<string>("DatabaseSettings:JsonFilePath");
    var dbService = new JsonDatabaseService(jsonPath);
    return new FinanceAppContext(dbService);
});

builder.Services.AddScoped<IExpensesService, ExpensesService>();
var app = builder.Build();

// Standard middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Make sure static files are enabled
app.UseRouting();
app.UseAuthorization();

// MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Expenses}/{action=Index}/{id?}");

app.Run();
