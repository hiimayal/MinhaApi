using MinhaApi.Models;
using MinhaApi.Dtos;
using MinhaApi.Services;

namespace MinhaApi.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioResponse> CriarUsuario(Usuario usuario);
}