using Godot;
using System;
using System.Collections.Generic;
using TerraNova.Common.HexGrids;
using TerraNova.Common;
using TerraNova.Godot;

namespace TerraNova.Godot.HexGrids.Units
{
    [View(typeof(HexGrid))]
    public class HexGridView : Node, IView
    {
        public HexGrid HexGrid => SimulationObject as HexGrid;
        public SimulationObject SimulationObject { get; set; }
        public ViewManager ViewManager { get; set; }
    }
}