using Godot;
using System;
using TerraNova.Utils;

namespace TerraNova.Hexgrid
{
    public class HexGridMultiMeshRenderer : MultiMeshInstance
    {
        /*public static readonly Basis TileTransform = new Basis()
        {
            Row0 = new Vector3((float)Math.Sin(Math.PI / 3f), 0f, 0f),
            Row2 = new Vector3(0f, 0f, 0.1f),
            Row1 = new Vector3((float)Math.Sin(Math.PI / 3f), 1.5f, 0f),
        };*/

        public static readonly Basis TileTransform = new Basis()
        {
            Row0 = new Vector3((float)Math.Sin(Math.PI / 3f) * 2, (float)-Math.Sin(Math.PI / 3f), 0f),
            Row2 = new Vector3(0f, -1.5f, 0f),
            Row1 = new Vector3(0f, 0f, 0.1f),
        };

        public override void _Ready()
        {
            if (this.TryGetParrentOfType<HexGrid>(out var pHexGrid))
            {
                pHexGrid.Renderer = this;
            }
            else
            {
                GD.PrintErr("Could not Link with Hexgrid. Not fould as Parrent.");
            }
        }

        public void Update(HexStorage<int> Storage)
        {
            this.Multimesh.InstanceCount = Storage.Count;

            var i = 0;
            foreach (var (xOffsetCoordinate, pValue) in Storage)
            {
                var xCubeCoordinate = xOffsetCoordinate.CubeCoordinate;
                var pPosition = TileTransform.Xform(new Vector3(xCubeCoordinate.X, xCubeCoordinate.Z, xCubeCoordinate.Z * 2 + xCubeCoordinate.X));

                this.Multimesh.SetInstanceTransform(i, new Transform(Quat.Identity, pPosition));
                i++;
            }
        }
    }
}