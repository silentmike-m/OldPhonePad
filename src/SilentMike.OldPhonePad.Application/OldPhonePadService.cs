namespace SilentMike.OldPhonePad.Application;

using System.Text;
using System.Text.RegularExpressions;
using SilentMike.OldPhonePad.Application.Commons.Exception;
using SilentMike.OldPhonePad.Application.Constants;

public static class OldPhonePadService
{
    private const string INPUT_PATTERN = "^[0-9* ]*#$";

    /// <summary>
    ///     Converts a sequence of keypad presses into text.
    /// </summary>
    /// <param name="input">Input sequence to be convert.</param>
    /// <returns>The converted text string. Returns empty string if input is null or empty.</returns>
    /// <exception cref="InvalidSequenceException">Thrown when the input sequence is invalid.</exception>
    /// <exception cref="SequenceInvalidCharException">
    ///     Thrown when the input contains invalid characters that can not be
    ///     mapped.
    /// </exception>
    public static string OldPhonePad(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        ValidateInput(input);

        char? currentChar = null;
        var currentCharCount = 0;
        var result = new StringBuilder();

        foreach (var inputChar in input)
        {
            switch (inputChar)
            {
                // Append current buffer to result on end of sequence or empty character
                case OldPhoneChars.HASH or OldPhoneChars.EMPTY_CHARACTER:
                {
                    Append(ref currentChar, ref currentCharCount, result);

                    break;
                }
                // Delete last character
                case OldPhoneChars.BACKSPACE:
                {
                    DeleteLastCharacter(ref currentChar, ref currentCharCount, result);

                    break;
                }
                default:
                {
                    // If is character is digit then process it
                    if (char.IsDigit(inputChar))
                    {
                        AppendDigit(ref currentChar, ref currentCharCount, inputChar, result);
                    }

                    break;
                }
            }
        }

        return result.ToString();
    }

    private static void Append(ref char? currentChar, ref int currentCharCount, StringBuilder result)
    {
        //If current char is null or not occurrence then do nothing
        if (currentChar is null || currentCharCount <= 0)
        {
            return;
        }

        var mappedChar = OldPhonePadKeyMapStrategy.GetCharacter(currentChar.Value, currentCharCount);

        if (mappedChar is null)
        {
            //If strategy doesn't contain mapping for given char then throw exception according to parameter value
            throw new SequenceInvalidCharException(currentChar.Value);
        }

        //Append mapped character to result and clear buffer
        result.Append(mappedChar);

        ClearBuffer(out currentChar, out currentCharCount);
    }

    private static void AppendDigit(ref char? currentChar, ref int currentCharCount, char input, StringBuilder result)
    {
        if (currentChar != input || currentCharCount == OldPhonePadKeyMapStrategy.GetMaxCharacterCount(input))
        {
            //If current char is same and it is max occurrence according to the strategy then append current buffer
            Append(ref currentChar, ref currentCharCount, result);

            //Set char as first occurrence
            currentChar = input;
            currentCharCount = 1;
        }
        else
        {
            //Otherwise increase occurrence
            currentChar = input;
            currentCharCount++;
        }
    }

    private static void ClearBuffer(out char? currentChar, out int currentCharCount)
    {
        currentChar = null;
        currentCharCount = 0;
    }

    private static void DeleteLastCharacter(ref char? currentChar, ref int currentCharCount, StringBuilder result)
    {
        if (currentChar is not null)
        {
            //If any key was pressed remove it from buffer
            ClearBuffer(out currentChar, out currentCharCount);
        }
        else if (result.Length > 0)
        {
            //Otherwise remove last character from result
            result.Length--;
        }
    }

    private static void ValidateInput(string input)
    {
        var inputRegex = new Regex(INPUT_PATTERN, RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(100));
        var inputMatch = inputRegex.Match(input);

        if (inputMatch.Success is false)
        {
            throw new InvalidSequenceException();
        }
    }
}
