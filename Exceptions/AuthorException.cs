namespace Library.Exceptions;

public class AuthorException : Exception
{
    public AuthorException() { }
    public AuthorException(string message) : base(message) { }
    public AuthorException(string message, Exception inner) : base(message, inner) { }
}