using Godot;
using TerraNova.Godot.Utils;
using TerraNova.Common.HexGrids.Coordinates;
using TerraNova.Common.HexGrids.Tiles;
using TerraNova.Godot.HexGrids.Coordinates;
using TerraNova.Common;

namespace TerraNova.Godot.HexGrids.Tiles
{
    [View(typeof(Tile))]
    public class TileView : Spatial, IView
    {
        public TileMesh TileMesh { get; set; }
        public Tile Tile { get { return (Tile)SimulationObject; } }
        public SimulationObject SimulationObject { get; set; }

        public override void _Process(float fDelta)
        {
            var pTile = Tile;

            if (pTile != null)
            {
                var xWorldCoordinates = pTile.Coordinate.GetWorldCoordinates();

                this.Translation = new Vector3(xWorldCoordinates.x, pTile.Height * 0.1f, xWorldCoordinates.y);
            }
        }
    }
}