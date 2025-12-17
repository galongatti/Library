namespace Library.Exceptions;

public class LendException : Exception
{
    public LendException() { }
    public LendException(string message) : base(message) { }
    public LendException(string message, Exception inner) : base(message, inner) { }
}

