using MinhaApi.Services;
using MinhaApi.Endpoints;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Interfaces;
using MinhaApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite: Bearer {seu token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<JwtService>();
var chave = "uma-chave-secreta-bem-grande-1234";

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(chave)
            ),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
           options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("ERRO JWT:");
                    Console.WriteLine(context.Exception.Message);

                    return Task.CompletedTask;
                }
            };
        });
    

builder.Services.AddAuthorization();

var app = builder.Build();

// Swagger só em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSwagger");

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.MapControllers();        // Controllers
app.MapProdutoEndpoints();
app.MapUsuarioEndpoints(); // Minimal APIs

app.Run();
public partial class Program { }
