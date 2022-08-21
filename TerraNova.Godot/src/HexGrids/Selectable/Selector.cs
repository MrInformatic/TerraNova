using Godot;
using TerraNova.Godot.Utils;

namespace TerraNova.Godot.HexGrids.Selectables
{
    public class Selector : MeshInstance
    {
        public ISelectable Selectable { get; private set; }

        private SpatialMaterial pMaterial;

        public override void _Ready()
        {
            base._Ready();

            if (this.TryGetParrentOfType<ISelectable>(out var pSelectable))
            {
                this.Selectable = pSelectable;
                pSelectable.Selector = this;
            }
        }

        public override void _Process(float fDelta)
        {
            this.Visible = Selectable != null && Selectable.ViewManager.SelectedObject == Selectable.SimulationObject;
        }
    }
}