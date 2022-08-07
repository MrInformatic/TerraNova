using Godot;
using System;
using TerraNova.Common.HexGrids.Coordinates;

namespace TerraNova.Godot.HexGrids.Coordinates
{
    public static class CubeCoordinateExt
    {
        public static readonly Vector2 TransformX = new Vector2((float)Math.Sin(Math.PI / 3f) * 2, 0f);
        public static readonly Vector2 TransformZ = new Vector2((float)-Math.Sin(Math.PI / 3f), -1.5f);
        public static readonly Vector2 SizeScaler = new Vector2((float)Math.Sin(Math.PI / 3f) * 2, -1.5f);

        public static Vector2 GetWorldCoordinates(this CubeCoordinate xCoordinate)
        {
            return TransformX * xCoordinate.X + TransformZ * xCoordinate.Z;
        }
    }
}