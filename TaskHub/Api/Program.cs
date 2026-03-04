using LoggingLibrary;
using DI;
namespace Api;

/// <summary>
/// Точка входа приложения
/// </summary>
public sealed class Program
{
    /// <summary>
    /// Запуск приложения
    /// </summary>
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseInfraSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build();
        var serviceProvider = host.Services;
        using (var scope = serviceProvider.CreateScope())
        {
            var scopeProvider = scope.ServiceProvider;
            scopeProvider.ResolveAndCompare<Singleton1>();
            scopeProvider.ResolveAndCompare<Singleton2>();
            scopeProvider.ResolveAndCompare<Scoped1>();
            scopeProvider.ResolveAndCompare<Scoped2>();
            scopeProvider.ResolveAndCompare<Transient1>();
            scopeProvider.ResolveAndCompare<Transient2>();
        }
        using (var scope = serviceProvider.CreateScope())
        {
            var scopeProvider = scope.ServiceProvider;
            scopeProvider.ResolveAndCompare<Singleton1>();
            scopeProvider.ResolveAndCompare<Singleton2>();
            scopeProvider.ResolveAndCompare<Scoped1>();
            scopeProvider.ResolveAndCompare<Scoped2>();
            scopeProvider.ResolveAndCompare<Transient1>();
            scopeProvider.ResolveAndCompare<Transient2>();
        }
        host.Run();

    }
}