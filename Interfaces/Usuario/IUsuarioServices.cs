using MinhaApi.Models;
using MinhaApi.Dtos;

namespace MinhaApi.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioResponse> CriarUsuario(Usuario usuario);
    Task<LoginResponse> LoginUsuario(LoginRequest login);
}