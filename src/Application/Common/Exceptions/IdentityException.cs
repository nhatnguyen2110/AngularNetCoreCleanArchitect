namespace CleanArchitecture.Application.Common.Exceptions;
public class IdentityException : Exception
{
    public IdentityException()
        : base()
    {
    }

    public IdentityException(string message)
        : base(message)
    {
    }

    public IdentityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}