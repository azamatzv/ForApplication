namespace ForApplication.Models.Exceptions;

public class AlreadyExistException : Exception
{
    public AlreadyExistException(string message)
        : base(message)
    {

    }
}