using System;

namespace TerraNova.Godot.Loading
{
    public interface IProgressProvider
    {
        double Progress { get; }

        bool IsFinished { get; }
    }
}