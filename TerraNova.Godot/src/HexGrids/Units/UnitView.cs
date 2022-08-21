using System;
using Godot;
using TerraNova.Common;
using TerraNova.Common.HexGrids.Units;
using TerraNova.Godot.HexGrids.Coordinates;

namespace TerraNova.Godot.HexGrids.Units
{
    [View(typeof(Unit))]
    public class UnitView : Spatial, IView
    {
        public Unit Unit { get { return (Unit)SimulationObject; } }

        public SimulationObject SimulationObject { get; set; }

        public override void _Process(float fDelta)
        {
            var pUnit = Unit;

            if (pUnit != null)
            {
                var xWorldCoordinates = pUnit.Coordinate.GetWorldCoordinates();
                var pTile = pUnit.HexGrid.Map[pUnit.Coordinate.OffsetCoordinate];

                this.Translation = new Vector3(xWorldCoordinates.x, pTile.Height * 0.1f, xWorldCoordinates.y);
            }
        }
    }
}