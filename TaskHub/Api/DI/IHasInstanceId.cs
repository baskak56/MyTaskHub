using System;
using System.IO;

namespace DI;

public interface IHasInstanceId
{
    Guid InstanceId { get; }
}
