using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace DI;

public static class ServiceExtensions
{
    public static void ResolveAndCompare<T>(this IServiceProvider services) where T : IHasInstanceId
	{
		var first = services.GetService<T>();
		var second = services.GetService<T>();
		Console.WriteLine($"{typeof(T).Name} {first.InstanceId}, {second.InstanceId}");
        Console.WriteLine( first.InstanceId  == second.InstanceId  ? "равны" : "неравны");
    }
}
