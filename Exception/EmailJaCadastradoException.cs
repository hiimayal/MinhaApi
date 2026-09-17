namespace MinhaApi.Exceptions;

public class EmailJaCadastradoException : Exception
{
    public EmailJaCadastradoException()
        : base("E-mail já cadastrado")
    {
    }
}