using System;
using Godot;
using TerraNova.Common;
using TerraNova.Common.HexGrids.Units;
using TerraNova.Godot.HexGrids.Coordinates;
using TerraNova.Godot.HexGrids.Selectables;

namespace TerraNova.Godot.HexGrids.Units
{
    [View(typeof(Unit))]
    public class UnitView : Spatial, IView, ISelectable
    {
        public Selector Selector { get; set; }

        public Unit Unit => SimulationObject as Unit;
        public SimulationObject SimulationObject { get; set; }
        public ViewManager ViewManager { get; set; }

        public override void _Process(float fDelta)
        {
            var pUnit = Unit;

            if (pUnit == null || !pUnit.IsValid)
            {
                return;
            }

            var pTile = pUnit.Tile;

            if (pTile == null)
            {
                return;
            }

            var xWorldCoordinates = pTile.Coordinate.GetWorldCoordinates();

            this.Translation = new Vector3(xWorldCoordinates.x, pTile.Height * 0.1f, xWorldCoordinates.y);
        }
    }
}