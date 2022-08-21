using Godot;
using TerraNova.Godot.Utils;

namespace TerraNova.Godot.HexGrids.Tiles
{
    public class TileCollider : StaticBody
    {
        public TileView TileView { get; private set; }

        private SpatialMaterial pMaterial;

        public override void _Ready()
        {
            base._Ready();

            if (this.TryGetParrentOfType<TileView>(out var pTileView))
            {
                this.TileView = pTileView;
                pTileView.TileCollider = this;
            }
        }

        public override void _InputEvent(Object camera, InputEvent e, Vector3 position, Vector3 normal, int shapeIdx)
        {
            this.TileView.Input(e);
        }
    }
}