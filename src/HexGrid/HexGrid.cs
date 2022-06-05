using Godot;
using System;

namespace TerraNova.Hexgrid
{
    public class HexGrid : Node
    {
        [Export] public int Width { get; set; }
        [Export] public int Height { get; set; }

        public HexStorage<int> Storage { get; set; }

        public HexGridMultiMeshRenderer Renderer { get; set; }

        public override void _Ready()
        {
            Storage = new HexStorage<int>(Width, Height);

            Renderer.Update(Storage);
        }
    }
}
