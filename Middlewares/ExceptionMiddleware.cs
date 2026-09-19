using System.Text.Json;
using MinhaApi.Exceptions;

namespace MinhaApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
         catch (EmailJaCadastradoException)
        {
            context.Response.StatusCode = 409;
            context.Response.ContentType = "application/json";

            var resposta = new
            {
                erro = "E-mail já cadastrado."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(resposta)
            );
        }
        catch (CredenciaisInvalidasException)
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var resposta = new
            {
                erro = "E-mail ou senha inválidos."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(resposta)
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var resposta = new
            {
                erro = "Ocorreu um erro interno no servidor."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(resposta)
            );
        }
    }
}