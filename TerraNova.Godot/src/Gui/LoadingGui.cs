using Godot;
using TerraNova.Godot.Utils;
using TerraNova.Godot.Loading;

namespace TerraNova.Godot.Gui
{
    public class LoadingGui : Control, IGui<IProgressProvider>
    {
        [Export] public NodePath LoadingRangePath { get; set; }
        public Range LoadingRange { get; private set; }

        private IProgressProvider pData;

        public override void _Ready()
        {
            if (this.TryGetParrentOfType<GameGui>(out var pGameGui))
            {
                pGameGui.RegisterGui(this);
            }

            LoadingRange = this.GetNode<Range>(LoadingRangePath);
            this.SetProcess(false);
        }

        public override void _Process(float delta)
        {
            if (this.pData != null)
            {
                if (this.LoadingRange != null)
                {
                    this.LoadingRange.Value = this.pData.Progress;
                }

                if (this.pData.IsFinished)
                {
                    this.Close();
                }
            }
        }

        public void Open(IProgressProvider pData)
        {
            this.Visible = true;
            this.SetProcess(true);
            this.pData = pData;
        }

        public void Close()
        {
            this.Visible = false;
            this.SetProcess(false);
            this.pData = null;
        }
    }
}