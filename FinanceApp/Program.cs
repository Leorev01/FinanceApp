using FinanceApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Register MVC
builder.Services.AddControllersWithViews();

// Register your JSON-based context
builder.Services.AddSingleton(provider =>
{
    string jsonPath = builder.Configuration.GetValue<string>("DatabaseSettings:JsonFilePath");
    var dbService = new JsonDatabaseService(jsonPath);
    return new FinanceAppContext(dbService);
});

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
