namespace Store.Shared.Exceptions;

public class NotFoundException(string? message = "Registro não encontrado.") : Exception(message)
{
    
}