namespace SilentMike.OldPhonePad.Application.OldPhonePad.Models;

using System.Text;
using SilentMike.OldPhonePad.Application.Commons.Exception;
using SilentMike.OldPhonePad.Application.Commons.Interfaces;
using SilentMike.OldPhonePad.Application.OldPhonePad.Constants;

internal sealed class OldPhonePadContext
{
    private readonly IKeyMapStrategy mapStrategy;
    private readonly StringBuilder result = new();
    private readonly bool skipInvalidChars;
    private int charCount = 0;
    private char? currentChar = null;

    public string Result => this.result.ToString();

    public OldPhonePadContext(IKeyMapStrategy mapStrategy, bool skipInvalidChars)
    {
        this.mapStrategy = mapStrategy;
        this.skipInvalidChars = skipInvalidChars;
    }

    public void Handle(char input)
    {
        switch (input)
        {
            // Append current buffer to result on end of sequence or empty character
            case OldPhoneChars.HASH or OldPhoneChars.EMPTY_CHARACTER:
            {
                this.Append();

                break;
            }
            // Delete last character
            case OldPhoneChars.BACKSPACE:
            {
                this.DeleteLastCharacter();

                break;
            }
            default:
            {
                // If is character is digit then process it
                if (char.IsDigit(input))
                {
                    this.AppendDigit(input);
                }
                else if (this.skipInvalidChars is false)
                {
                    // Throw exception if it is invalid character
                    throw new SequenceInvalidCharException(input);
                }

                break;
            }
        }
    }

    private void Append()
    {
        //If current char is null or not occurrence then do nothing
        if (this.currentChar is null || this.charCount <= 0)
        {
            return;
        }

        var mappedChar = this.mapStrategy.GetCharacter(this.currentChar.Value, this.charCount);

        if (mappedChar is null)
        {
            //If strategy doesn't contain mapping for given char then throw exception according to parameter value
            if (this.skipInvalidChars)
            {
                return;
            }

            throw new SequenceInvalidCharException(this.currentChar.Value);
        }

        //Append mapped character to result and clear buffer
        this.result.Append(mappedChar);

        this.ClearBuffer();
    }

    private void AppendDigit(char digit)
    {
        if (this.currentChar != digit || this.charCount == this.mapStrategy.GetMaxCharacterCount(digit))
        {
            //If current char is same and it is max occurrence according to the strategy then append current buffer
            this.Append();

            //Set char as first occurrence
            this.currentChar = digit;
            this.charCount = 1;
        }
        else
        {
            //Otherwise increase occurrence
            this.currentChar = digit;
            this.charCount++;
        }
    }

    private void ClearBuffer()
    {
        this.currentChar = null;
        this.charCount = 0;
    }

    private void DeleteLastCharacter()
    {
        if (this.currentChar is not null)
        {
            //If any key was pressed remove it from buffer
            this.ClearBuffer();
        }
        else if (this.result.Length > 0)
        {
            //Otherwise remove last character from result
            this.result.Length--;
        }
    }
}
