using MinhaApi.Models;
using MinhaApi.Dtos;
using System.ComponentModel.DataAnnotations;
using MinhaApi.Interfaces;

namespace MinhaApi.Endpoints;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this WebApplication app)
    {
        app.MapPost("/usuarios", async (CriarUsuarioRequest user, IUsuarioService service) => {
            var contexto = new ValidationContext(user);
            var erros = new List<ValidationResult>();

            var valido = Validator.TryValidateObject(
                user,
                contexto,
                erros,
                validateAllProperties: true
            );

            if (!valido)
            {
                return Results.BadRequest(erros);
            }

            var usuario = new Usuario{
                Nome = user.Nome,
                Email = user.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(user.Senha),
                Role = "Cliente"
            };
            var usuarioCriado = await service.CriarUsuario(usuario);
            return Results.Created("/usuarios", usuarioCriado);
        });

         app.MapPost("/login", async (LoginRequest login, IUsuarioService service) =>
         {
             var contexto = new ValidationContext(login);
            var erros = new List<ValidationResult>();

            var valido = Validator.TryValidateObject(
                login,
                contexto,
                erros,
                validateAllProperties: true
            );
            if (!valido)
            {
                return Results.BadRequest(erros);
            }

            var resposta = await service.LoginUsuario(login);
            return Results.Ok(resposta);

         });
    }
}

       
