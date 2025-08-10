using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Tratativa para conexão com o banco de dados
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(defaultConnectionString))
{
    throw new InvalidOperationException("The connection string 'DefaultConnection' is not configured.");
}

builder.Services.AddHealthChecks()
    .AddSqlServer(defaultConnectionString)
    .AddRabbitMQ(factory: serviceProvider =>
    {
        var connectionFactory = new ConnectionFactory()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };
        return connectionFactory.CreateConnectionAsync();
    });

//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Appointment Scheduler API is running.");

app.UseHealthChecks("/health");
//app.UseAuthorization();
//app.MapControllers();

app.Run();
