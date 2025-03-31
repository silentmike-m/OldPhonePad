using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SilentMike.OldPhonePad.Application;
using SilentMike.OldPhonePad.Application.Commons.Interfaces;

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

    var serviceProvider = new ServiceCollection()
        .AddApplication()
        .BuildServiceProvider();

    // create old phone pad converting service
    // in future consider keyed DI
    var oldPhonePadService = serviceProvider.GetRequiredService<IPhonePadService>();
    var oldPhonePadKeyMapStrategy = serviceProvider.GetRequiredService<IKeyMapStrategy>();

    Process(oldPhonePadService, oldPhonePadKeyMapStrategy);

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

static void Process(IPhonePadService service, IKeyMapStrategy keyMapStrategy)
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
            var result = service.Convert(input, keyMapStrategy, false);
            Console.WriteLine($"Result: {result}");
        }
        catch (Exception exception)
        {
            Log.Error(exception, exception.Message);
        }
    }
}