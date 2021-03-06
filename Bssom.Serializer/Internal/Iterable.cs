﻿//using System.Runtime.CompilerServices;

using System.Collections.Generic;

namespace Bssom.Serializer.Internal
{
    internal interface Iterable<T>
    {
        IEnumerable<T> Ts { get; }
        int Length { get; }
        ref T GetFirstElementReference(out bool isContiguousMemoryArea);
    }
}
