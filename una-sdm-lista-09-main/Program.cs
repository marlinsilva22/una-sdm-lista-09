using EleicaoBrasilApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 👇 ATIVA CONTROLLERS (ESSENCIAL)
builder.Services.AddControllers();

// Banco In-Memory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("EleicaoDb"));

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// 👇 MAPEAR CONTROLLERS (ESSENCIAL)
app.MapControllers();

app.MapGet("/", () => "API rodando");

app.Run();