using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogTracker.Data;
using BlogEventTracker.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlogTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogTrackerContext") ?? throw new InvalidOperationException("Connection string 'BlogTrackerContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<BlogTrackerContext>();

    // Check if there are any existing admin records
    if (!dbContext.AdminInfo.Any())
    {
        // Create a new AdminInfo instance with email and password
        var admin = new AdminInfo
        {
            EmailId = "admin@example.com",
            Password = "adminpassword" // You should hash the password in a real application
        };

        // Add the admin record to the database
        dbContext.AdminInfo.Add(admin);
        dbContext.SaveChanges();
    }
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "adminLogin",
        pattern: "admin/login",
        defaults: new { controller = "AdminInfo", action = "Login" });

    endpoints.MapControllerRoute(
    name: "employeeLogin",
    pattern: "employee/login",
    defaults: new { controller = "EmpInfo", action = "EmployeeLogin" });

    endpoints.MapControllerRoute(
        name: "employeeLogout",
        pattern: "employee/logout",
        defaults: new { controller = "EmpInfo", action = "Logout" });



    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=BlogInfoes}/{action=Index}/{id?}");
});

app.Run();
