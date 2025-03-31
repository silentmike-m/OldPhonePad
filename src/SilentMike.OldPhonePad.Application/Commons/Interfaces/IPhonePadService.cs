namespace SilentMike.OldPhonePad.Application.Commons.Interfaces;

/// <summary>
///     Defines the public contract for phone keypad input sequences into text.
/// </summary>
public interface IPhonePadService
{
    /// <summary>
    ///     Converts a sequence of keypad presses into text.
    /// </summary>
    /// <param name="input">Input sequence to be convert.</param>
    /// <param name="keyMapStrategy">Strategy that should be used to converted input.</param>
    /// <param name="skipInvalidChars">Skip invalid character or throw exception</param>
    /// <returns>The converted text string. Returns empty string if input is null or empty.</returns>
    /// <exception cref="SequenceInvalidEndException">Thrown when the input contains invalid end of sequence.</exception>
    /// <exception cref="SequenceInvalidCharException">Thrown when the input contains invalid characters.</exception>
    string Convert(string input, IKeyMapStrategy keyMapStrategy, bool skipInvalidChars);
}
