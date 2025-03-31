namespace SilentMike.OldPhonePad.Application.Commons.Interfaces;

/// <summary>
/// Defines the public contract for the keys mapping strategy.
/// </summary>
public interface IKeyMapStrategy
{
    /// <summary>
    /// Maps a key press to its corresponding character based on the number of presses.
    /// </summary>
    /// <param name="key">The key that was pressed.</param>
    /// <param name="presses">The number of times the key was pressed.</param>
    /// <returns>The mapped character for a given key or null when key was not found.</returns>
    char? GetCharacter(char key, int presses);

    /// <summary>
    /// Gets the max number of characters for a given key.
    /// </summary>
    /// <param name="key">The key that was pressed.</param>
    /// <returns>The max number of characters for a given key or null when key was not found.</returns>
    int? GetMaxCharacterCount(char key);
}
