using Godot;
using TerraNova.Godot.Utils;
using TerraNova.Common.HexGrids.Coordinates;
using TerraNova.Common.HexGrids.Tiles;
using TerraNova.Common.HexGrids.Units;
using TerraNova.Godot.HexGrids.Coordinates;
using TerraNova.Common;
using System.Collections.Generic;

namespace TerraNova.Godot.HexGrids.Tiles
{
    [View(typeof(Tile))]
    public class TileView : Spatial, IView
    {
        public TileMesh TileMesh { get; set; }
        public TileCollider TileCollider { get; set; }

        public Tile Tile => SimulationObject as Tile;
        public SimulationObject SimulationObject { get; set; }
        public ViewManager ViewManager { get; set; }

        public override void _Process(float fDelta)
        {
            var pTile = Tile;

            if (pTile != null)
            {
                var xWorldCoordinates = pTile.Coordinate.GetWorldCoordinates();

                this.Translation = new Vector3(xWorldCoordinates.x, pTile.Height * 0.1f, xWorldCoordinates.y);
            }
        }

        public void Input(InputEvent e)
        {
            if (e.IsActionPressed("select"))
            {
                if (Tile.Unit != null)
                {
                    ViewManager.SelectedObject = Tile.Unit;
                }
                else
                {
                    ViewManager.SelectedObject = null;
                }
            }

            if (e.IsActionPressed("move"))
            {
                if (ViewManager.SelectedObject is Unit pUnit)
                {
                    var pPath = new List<CubeCoordinate>();
                    Tile.HexGrid?.MoveUnit(pUnit, Tile.Coordinate, ref pPath);
                }
            }
        }
    }
}