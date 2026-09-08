using System.Text.Json;

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
        catch (Exception)
        {
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