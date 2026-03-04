using System;
using System.IO;

namespace DI;

public class DisposedService : IDisposable
{
	public Guid InstanceId { get; }
    private bool disposed = false;

    public DisposedService()
	{
		var id = Guid.NewGuid();
		this.InstanceId = id;
		Console.WriteLine($"создание объекта: {this.InstanceId} {this.GetType().Name}");
	}
	public void Dispose()
	{
        GC.SuppressFinalize(this);
        Console.WriteLine($"уничтожение объекта: {this.InstanceId} {this.GetType().Name}");
	}
}
