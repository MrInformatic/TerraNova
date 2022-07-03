using Godot;
using TerraNova.Utils;

namespace TerraNova.HexGrids.Tiles
{
    public class TileMesh : MeshInstance
    {
        public TileView TileView { get; private set; }

        public override void _Ready()
        {
            base._Ready();

            if (this.TryGetParrentOfType<TileView>(out var pTileView))
            {
                this.TileView = pTileView;
                pTileView.TileMesh = this;

                var fIntensity = pTileView.Tile.Height / 40.0f;

                var pMaterial = new SpatialMaterial();

                pMaterial.AlbedoColor = new Color(fIntensity, fIntensity, fIntensity, 1f);

                this.MaterialOverride = pMaterial;
            }
        }
    }
}