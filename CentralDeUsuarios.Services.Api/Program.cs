using CentralDeUsuarios.Infra.Messages.Consumers;
using CentralDeUsuarios.Services.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Setup.AddRegisterServices(builder);
Setup.AddEntityFrameworkServices(builder);
Setup.AddMessageServices(builder);
Setup.AddAutoMapperServices(builder);
Setup.AddMongoDBServices(builder);

//ativando o consumidor da mensageria
builder.Services.AddHostedService<MessageQueueConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }