using System;
using System.IO;

namespace DI;

public class Singleton1 : DisposedService, IHasInstanceId
{
	public Singleton1()
	{
	}
}

public class Singleton2 : DisposedService, IHasInstanceId
{
    public Singleton2()
    {
    }
}

public class Scoped1 : DisposedService, IHasInstanceId
{
    public Scoped1()
    {
    }
}

public class Scoped2 : DisposedService, IHasInstanceId
{
    public Scoped2()
    {
    }
}
public class Transient1 : DisposedService, IHasInstanceId
{
    public Transient1()
    {
    }
}

public class Transient2 : DisposedService, IHasInstanceId
{
    public Transient2()
    {
    }
}