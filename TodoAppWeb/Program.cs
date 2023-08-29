using Microsoft.EntityFrameworkCore;
using TodoAppWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register controllers with Views
builder.Services.AddControllersWithViews();

// Register MS SQL server database context via EntityFramework.Core
builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoContext")));

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

builder.Services.AddRouting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

//Map controller routes
app.MapControllerRoute(
       name: "default",
          pattern: "{controller=ToDo}/{action=Index}/{id?}");

app.UseAuthorization();

app.MapRazorPages();

app.Run();
