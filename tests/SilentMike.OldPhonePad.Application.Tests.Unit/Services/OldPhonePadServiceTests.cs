namespace SilentMike.OldPhonePad.Application.Tests.Unit.Services;

using SilentMike.OldPhonePad.Application.Commons.Exception;

[TestClass]
public sealed class OldPhonePadServiceTests
{
    [DataTestMethod, DataRow(""), DataRow("  ")]
    public void Convert_Should_ReturnEmptyString_When_InputIsEmpty(string input)
    {
        // Arrange

        //Act
        var result = OldPhonePadService.OldPhonePad(input);

        // Assert
        result.Should()
            .BeEmpty();
    }

    [DataTestMethod, DataRow("33#", "E"), DataRow("227*#", "B"), DataRow("4433555 555666#", "HELLO"), DataRow("8 88777444666*664#", "TURING"), DataRow("808#", "T T"), DataRow("2222#", "CA"), DataRow("222 2 22#", "CAB")]
    public void Convert_Should_ReturnProperText(string input, string expected)
    {
        // Arrange

        // Act
        var result = OldPhonePadService.OldPhonePad(input);

        // Assert
        result.Should()
            .Be(expected);
    }

    [DataTestMethod, DataRow("334"), DataRow("33AB#")]
    public void Convert_Should_ThrowSequenceInvalidEndException_When_InputNotEndsWithSharp(string input)
    {
        // Arrange

        // Act
        var result = () => OldPhonePadService.OldPhonePad(input);

        // Assert
        result.Should()
            .ThrowExactly<InvalidSequenceException>();
    }
}
