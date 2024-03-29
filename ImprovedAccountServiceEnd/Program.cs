using BankAccountTransactionsEnd.Services;
using ImprovedAccountServiceEnd.Services;
using Microsoft.EntityFrameworkCore;
using SkysFormsDemo.Data;
using SkysFormsDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<DataInitializer>();
builder.Services.AddTransient<IPersonService,PersonService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAccountService2, AccountService2>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitializer>().SeedData();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
