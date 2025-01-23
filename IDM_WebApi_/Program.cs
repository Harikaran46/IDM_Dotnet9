//Author :
//last Updated:
//Reviewed by:

using IDMApi.Services;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;

// register services in the DI container
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient(); //making HTTP requests to external services.

builder.Services.AddScoped<IEmployeeServices, Employeeservices>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<ITaskServices, TaskServices>();

//enabling the application to use Entity Framework Core with a SQL Server connection.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//registers controllers, making them available for your API to handle HTTP requests.
builder.Services.AddControllers();

//set up Swagger,This will allow you to view and test your API endpoints in a user-friendly interface.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod());
});

var app = builder.Build();

//Swagger is enabled via app.UseSwagger() and app.UseSwaggerUI(). This provides an interactive API documentation UI.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowAll");
//maps the routes for all the controllers in the application so that HTTP requests can be handled appropriately.
app.MapControllers();

app.Run();
