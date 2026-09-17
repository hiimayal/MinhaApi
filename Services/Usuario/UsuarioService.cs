using MinhaApi.Models;
using MinhaApi.Dtos;
using MinhaApi.Interfaces;
using MinhaApi.Exceptions;

namespace MinhaApi.Services;

public class UsuarioService : IUsuarioService
{
    private readonly LojaDbContext context;
    public UsuarioService(LojaDbContext context)
    {
        this.context = context;
    }

    public async Task<UsuarioResponse> CriarUsuario(Usuario usuario)
    {
        if (UsuarioExiste(usuario.Email))
        {
            throw new EmailJaCadastradoException();
        }

        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        return new UsuarioResponse
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
        };
    }

    public bool UsuarioExiste(string email)
    {
        return context.Usuarios.Any(u => u.Email == email);
    }   
}