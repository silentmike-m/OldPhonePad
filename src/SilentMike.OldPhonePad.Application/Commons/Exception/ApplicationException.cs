namespace SilentMike.OldPhonePad.Application.Commons.Exception;

using Exception = System.Exception;

public abstract class ApplicationException : Exception
{
    protected ApplicationException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
