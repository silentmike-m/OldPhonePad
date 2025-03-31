namespace SilentMike.OldPhonePad.Application.Commons.Exception;

using Exception = System.Exception;

public sealed class InvalidSequenceException : ApplicationException
{
    public InvalidSequenceException(Exception? innerException = null)
        : base($"Sequence is invalid", innerException)
    {
    }
}
