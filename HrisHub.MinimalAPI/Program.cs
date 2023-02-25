using Microsoft.EntityFrameworkCore;
using HrisHub.Dal;
using HrisHub.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HrisHubDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConStr")));

builder.Services.AddScoped<ICommonRepository<Employee>, CommonRepository<Employee>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/employees", async (ICommonRepository<Employee> repository) =>
{
    var employees = await repository.GetAll();

    return employees.Count == 0 ? Results.NotFound() : Results.Ok(employees);
})
.WithName("GetAll")
.Produces<IEnumerable<Employee>>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapGet("/api/employees/{id}", async (int id, ICommonRepository<Employee> repository) =>
{
    var employee = await repository.GetDetails(id);

    return employee == null ? Results.NotFound() : Results.Ok(employee);
})
.WithName("GetDetails")
.Produces<Employee>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/api/employees", async (Employee employee, ICommonRepository<Employee> repository) =>
{
    var result = await repository.Insert(employee);

    return result == null ? Results.BadRequest() : Results.Created($"/api/employees/{result.Id}", result);
})
.WithName("Create")
.Produces<Employee>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

app.Run();
