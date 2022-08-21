using Godot;
using TerraNova.Godot.Utils;

namespace TerraNova.Godot.HexGrids.Tiles
{
    public class TileSelector : MeshInstance
    {
        public TileView TileView { get; private set; }

        private SpatialMaterial pMaterial;

        public override void _Ready()
        {
            base._Ready();

            if (this.TryGetParrentOfType<TileView>(out var pTileView))
            {
                this.TileView = pTileView;
                pTileView.TileSelector = this;
            }
        }

        public override void _Process(float fDelta)
        {
            this.Visible = this.TileView.Selected;
        }
    }
}