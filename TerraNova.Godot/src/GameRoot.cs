using Godot;
using System.Collections.Generic;
using System.Linq;
using System;
using TerraNova.Godot.Gui;
using TerraNova.Godot.Loading;

namespace TerraNova.Godot
{
    public class GameRoot : Node
    {
        public MultiProgressProvider ProgressProvider { get; } = new MultiProgressProvider();
        public GameGui GameGui { get; set; }
        public ViewManager ViewManager { get; set; }

        public override void _Ready()
        {
            GameGui.Open<LoadingGui, IProgressProvider>(ProgressProvider);
        }
    }
}