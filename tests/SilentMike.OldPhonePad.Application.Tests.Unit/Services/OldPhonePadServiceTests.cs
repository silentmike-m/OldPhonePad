namespace SilentMike.OldPhonePad.Application.Tests.Unit.Services;

using SilentMike.OldPhonePad.Application.Commons.Exception;
using SilentMike.OldPhonePad.Application.Commons.Strategies;
using SilentMike.OldPhonePad.Application.OldPhonePad.Services;

[TestClass]
public sealed class OldPhonePadServiceTests
{
    private readonly OldPhonePadService service = new();

    [TestMethod]
    public void Convert_Should_NotThrowException_When_InputNotContainsNewPhonePadChars()
    {
        // Arrange
        const string input = "33AB#44#";

        // Act
        var result = this.service.Convert(input, new OldPhonePadKeyMapStrategy(), skipInvalidChars: true);

        // Assert
        const string expectedResult = "EH";

        result.Should()
            .Be(expectedResult);
    }

    [DataTestMethod, DataRow(""), DataRow("  ")]
    public void Convert_Should_ReturnEmptyString_When_InputIsEmpty(string input)
    {
        // Arrange

        //Act
        var result = this.service.Convert(input, new OldPhonePadKeyMapStrategy(), skipInvalidChars: false);

        // Assert
        result.Should()
            .BeEmpty();
    }

    [DataTestMethod, DataRow("33#", "E"), DataRow("227*#", "B"), DataRow("4433555 555666#", "HELLO"), DataRow("8 88777444666*664#", "TURING"), DataRow("808#", "T T"), DataRow("2222#", "CA")]
    public void Convert_Should_ReturnProperText(string input, string expected)
    {
        // Arrange

        // Act
        var result = this.service.Convert(input, new OldPhonePadKeyMapStrategy(), skipInvalidChars: false);

        // Assert
        result.Should()
            .Be(expected);
    }

    [TestMethod]
    public void Convert_Should_ThrowSequenceInvalidCharException_When_InputNotContainsOldPhonePadChars()
    {
        // Arrange
        const string input = "33AB#";

        // Act
        var result = () => this.service.Convert(input, new OldPhonePadKeyMapStrategy(), skipInvalidChars: false);

        // Assert
        result.Should()
            .ThrowExactly<SequenceInvalidCharException>();
    }

    [TestMethod]
    public void Convert_Should_ThrowSequenceInvalidEndException_When_InputNotEndsWithSharp()
    {
        // Arrange
        const string input = "334";

        // Act
        var result = () => this.service.Convert(input, new OldPhonePadKeyMapStrategy(), skipInvalidChars: false);

        // Assert
        result.Should()
            .ThrowExactly<SequenceInvalidEndException>();
    }
}
