namespace Primeflix.Application.Common.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("Coult not find the user")
    {
    }

    public UserNotFoundException(string message) : base(message)
    {
    }
}