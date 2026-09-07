using MinhaApi.Services;
using MinhaApi.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Banco de dados
builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseSqlServer(
        "Server=LAPTOP-1MVO0EDU\\SQLEXPRESS;Database=LojaDB;Trusted_Connection=True;TrustServerCertificate=True;"
    ));

// CORS — permitindo Swagger acessar tanto HTTP quanto HTTPS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwagger", policy =>
    {
        policy
            .WithOrigins("http://localhost:5030", "https://localhost:7030")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Serviços da aplicação
builder.Services.AddScoped<ProdutoService>();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger só em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSwagger");

// Endpoints
app.MapControllers();        // Controllers
app.MapProdutoEndpoints();   // Minimal APIs

app.Run();
