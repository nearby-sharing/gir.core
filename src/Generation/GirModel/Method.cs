﻿using System.Collections.Generic;

namespace GirModel
{
    public interface Method
    {
        ComplexType Parent { get; }
        string Name { get; }
        ReturnType ReturnType { get; }
        string CIdentifier { get; }
        InstanceParameter InstanceParameter { get; }
        IEnumerable<Parameter> Parameters { get; }
        bool Introspectable { get; }
        Property? GetProperty { get; }
        Property? SetProperty { get; }
        string? Version { get; }

        bool IsUnref() => Name == "unref";
        bool IsFree() => Name == "free";
        bool IsAccessor() => GetProperty is not null || SetProperty is not null;
    }
}
