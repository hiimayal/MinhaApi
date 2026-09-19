using MinhaApi.Models;
using MinhaApi.Dtos;
using MinhaApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Exceptions;

namespace MinhaApi.Services;

public class UsuarioService : IUsuarioService
{
    private readonly LojaDbContext context;
    private readonly JwtService jwtService;

    public UsuarioService(LojaDbContext context, JwtService jwtService)
    {
        this.context = context;
        this.jwtService = jwtService;
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

    public async Task<LoginResponse> LoginUsuario(LoginRequest login)
    {
        var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == login.Email);

        if (usuario == null)
        {
            throw new CredenciaisInvalidasException();
        }                                           
                                                                    
        bool senhaCorreta = BCrypt.Net.BCrypt.Verify(
            login.Senha,
            usuario.SenhaHash
        );

        if (!senhaCorreta)
        {
            throw new CredenciaisInvalidasException();
        }
        var token = jwtService.GerarToken(usuario);
        return new LoginResponse
        {
            Token = token
        };

    }

    private bool UsuarioExiste(string email)
    {
        return context.Usuarios.Any(u => u.Email == email);
    }   
}