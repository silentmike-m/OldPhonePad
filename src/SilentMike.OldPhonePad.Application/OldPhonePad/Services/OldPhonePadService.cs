namespace SilentMike.OldPhonePad.Application.OldPhonePad.Services;

using SilentMike.OldPhonePad.Application.Commons.Exception;
using SilentMike.OldPhonePad.Application.Commons.Interfaces;
using SilentMike.OldPhonePad.Application.OldPhonePad.Models;

internal sealed class OldPhonePadService : IPhonePadService
{
    public string Convert(string input, IKeyMapStrategy keyMapStrategy, bool skipInvalidChars)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        if (input[^1] != '#')
        {
            throw new SequenceInvalidEndException();
        }

        var context = new OldPhonePadContext(keyMapStrategy, skipInvalidChars);

        foreach (var inputChar in input)
        {
            context.Handle(inputChar);
        }

        return context.Result;
    }
}
