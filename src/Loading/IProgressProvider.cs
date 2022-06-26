using System;

namespace TerraNova.Loading
{
    public interface IProgressProvider
    {
        double Progress { get; }

        bool IsFinished { get; }
    }
}