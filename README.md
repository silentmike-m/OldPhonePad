# OldPhonePad Converter

## Input format
- numbers: 0-9
-  *: as backspace
- space: pause between characters
- #: for end sequence

## Examples
```csharp
service.Convert("33#") // "B"
service.Convert("227*#") // "B"
service.Convert("4433555 555666#") // "HELLO""
service.Convert("8 88777444666*664#") // "TURING"
```

## Project Structure
- `src/SilentMike.OldPhonePad.Application`: Application class library. Contains core service 
- `src/SilentMike.OldPhonePad.Application.ConsoleApp`: Console application demo
- `tests/SilentMike.OldPhonePad.Application.Tests.Unit`: Unit tests