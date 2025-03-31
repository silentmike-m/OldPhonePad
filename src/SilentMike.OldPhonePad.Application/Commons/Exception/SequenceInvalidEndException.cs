namespace SilentMike.OldPhonePad.Application.Commons.Exception;

using Exception = System.Exception;

public sealed class SequenceInvalidEndException : ApplicationException
{
    public SequenceInvalidEndException(Exception? innerException = null)
        : base("Sequence contains invalid end", innerException)
    {
    }
}
