namespace SilentMike.OldPhonePad.Application;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SilentMike.OldPhonePad.Application.Commons.Interfaces;
using SilentMike.OldPhonePad.Application.Commons.Strategies;
using SilentMike.OldPhonePad.Application.OldPhonePad.Services;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //TODO: consider key map strategy as keyed DI
        services.AddSingleton<IPhonePadService, OldPhonePadService>();
        services.AddSingleton<IKeyMapStrategy, OldPhonePadKeyMapStrategy>();

        return services;
    }
}
