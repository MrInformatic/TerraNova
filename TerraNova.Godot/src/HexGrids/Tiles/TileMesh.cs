using Godot;
using TerraNova.Godot.Utils;

namespace TerraNova.Godot.HexGrids.Tiles
{
    public class TileMesh : MeshInstance
    {
        public TileView TileView { get; private set; }

        private SpatialMaterial pMaterial;

        public override void _Ready()
        {
            base._Ready();

            if (this.TryGetParrentOfType<TileView>(out var pTileView))
            {
                this.TileView = pTileView;
                pTileView.TileMesh = this;

                pMaterial = new SpatialMaterial();
                this.MaterialOverride = pMaterial;
            }
        }

        public override void _Process(float fDelta)
        {
            var fIntensity = TileView.Tile.Height / 40.0f;
            pMaterial.AlbedoColor = new Color(fIntensity, fIntensity, fIntensity, 1f);
        }
    }
}