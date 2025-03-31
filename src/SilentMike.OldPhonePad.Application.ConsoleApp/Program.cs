using Microsoft.Extensions.Configuration;
using Serilog;
using SilentMike.OldPhonePad.Application;

const int EXIT_FAILURE = 1;
const int EXIT_SUCCESS = 0;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    Log.Information("Hello to Phone Pad converter!");

    Process();

    return EXIT_SUCCESS;
}
catch (Exception exception)
{
    Log.Fatal(exception, "App terminated unexpectedly");

    return EXIT_FAILURE;
}
finally
{
    Log.CloseAndFlush();
}

static void Process()
{
    while (true)
    {
        Console.WriteLine("Enter sequence or empty to quit");
        var input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            break;
        }

        try
        {
            var result = OldPhonePadService.OldPhonePad(input);
            Console.WriteLine($"Result: {result}");
        }
        catch (Exception exception)
        {
            Log.Error(exception, exception.Message);
        }
    }
}
