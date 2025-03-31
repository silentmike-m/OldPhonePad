namespace SilentMike.OldPhonePad.Application.Constants;

/// <summary>
///     Defines old phone pad keys mapping
/// </summary>
internal static class OldPhonePadKeyMapStrategy
{
    private static readonly Dictionary<char, char[]> MAP = new()
    {
        {
            '1', [
                '&',
                '\'',
                '(',
            ]
        },
        {
            '2', [
                'A',
                'B',
                'C',
            ]
        },
        {
            '3', [
                'D',
                'E',
                'F',
            ]
        },
        {
            '4', [
                'G',
                'H',
                'I',
            ]
        },
        {
            '5', [
                'J',
                'K',
                'L',
            ]
        },
        {
            '6', [
                'M',
                'N',
                'O',
            ]
        },
        {
            '7', [
                'P',
                'Q',
                'R',
                'S',
            ]
        },
        {
            '8', [
                'T',
                'U',
                'V',
            ]
        },
        {
            '9', [
                'W',
                'X',
                'Y',
                'Z',
            ]
        },
        {
            '0', [
                ' ',
            ]
        },
    };

    public static char? GetCharacter(char key, int presses)
    {
        if (MAP.TryGetValue(key, out var chars) is false)
        {
            return null;
        }

        var index = (presses - 1) % chars.Length;

        return chars[index];
    }

    public static int? GetMaxCharacterCount(char key)
        => MAP.TryGetValue(key, out var chars)
            ? chars.Length
            : null;
}
