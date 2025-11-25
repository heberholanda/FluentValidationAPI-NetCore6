using FluentValidation;
using FluentValidationAPI.Validation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();

// Register FluentValidation validators automatically
// Starting from version 11.0, AddFluentValidation has been deprecated
// The recommended approach is to register validators manually
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Configure Swagger/OpenAPI - Learn more at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
