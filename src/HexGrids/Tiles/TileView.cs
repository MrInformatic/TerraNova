using Godot;
using TerraNova.Utils;
using TerraNova.HexGrids.Coordinates;

namespace TerraNova.HexGrids.Tiles
{
    public class TileView : Spatial
    {
        public TileMesh TileMesh { get; set; }
        public HexGridView HexGrid { get; private set; }
        public Tile Tile { get; set; }
        public CubeCoordinate Coordinate { get; set; }

        public override void _Ready()
        {
            base._Ready();

            if (this.TryGetParrentOfType<HexGridView>(out var pHexGrid))
            {
                this.HexGrid = pHexGrid;
            }
        }

        public void UpdateView()
        {
            if (Tile != null)
            {
                var xWorldCoordinates = Coordinate.WorldCoordinates;

                this.Translation = new Vector3(xWorldCoordinates.x, Tile.Height * 0.1f, xWorldCoordinates.y);
            }
        }
    }
}