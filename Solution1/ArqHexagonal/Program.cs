using ApplicationHexagonal.Interfaces;
using ApplicationHexagonal.Services;
using DomainHexagonal.Repositories;
using InfraHexagonal.Caching.Services;
using InfraHexagonal.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura a string de conexão com o banco de dados
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ICacheServices, CacheServices>();
builder.Services.AddStackExchangeRedisCache(o =>
{
    o.InstanceName = "INSTANCE";
    o.Configuration = "localhost:6379";
});

// Configura as injeções de dependência para repositório e serviço
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Adiciona suporte para controladores
builder.Services.AddControllers();

// Add services to the container.
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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
