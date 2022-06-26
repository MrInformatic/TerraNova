using Godot;
using System.Collections.Generic;
using System.Linq;
using System;
using TerraNova.Gui;
using TerraNova.Loading;

namespace TerraNova
{
    public class GameState : Node
    {
        public MultiProgressProvider ProgressProvider { get; } = new MultiProgressProvider();
        public GameGui GameGui { get; set; }

        public override void _Ready()
        {
            GameGui.Open<LoadingGui, IProgressProvider>(ProgressProvider);
        }
    }
}