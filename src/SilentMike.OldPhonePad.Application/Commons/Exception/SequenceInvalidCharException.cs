namespace SilentMike.OldPhonePad.Application.Commons.Exception;

using Exception = System.Exception;

public sealed class SequenceInvalidCharException : ApplicationException
{
    public SequenceInvalidCharException(char character, Exception? innerException = null)
        : base($"Sequence contains invalid character: {character}", innerException)
    {
    }
}
