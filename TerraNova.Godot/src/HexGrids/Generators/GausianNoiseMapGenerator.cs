using Godot;
using Godot.Collections;
using System.Text.RegularExpressions;
using System;
using TerraNova.Common.HexGrids;
using TerraNova.Common.HexGrids.Tiles;
using TerraNova.Common.HexGrids.Coordinates;
using TerraNova.Common.HexGrids.Units;
using TerraNova.Godot.HexGrids.Coordinates;
using TerraNova.Common;
using TerraNova.Godot.Utils;

namespace TerraNova.Godot.HexGrids.Generators
{
    public class GausianNoiseMapGenerator : Node
    {
        public static readonly Random Random = new Random();

        public struct GausianNoiseLevel
        {
            public float LevelMultiplier;
            public float CoordinateMultiplier;
        }

        [Export] public int Width { get; set; }
        [Export] public int Height { get; set; }

        [Export] public int BaseWidth { get; set; }
        [Export] public int BaseHeight { get; set; }

        [Export] public int LevelCount { get; set; }
        [Export] public float HeightMultipier { get; set; }

        private float[,] BaseNoise;

        public override void _Ready()
        {
            if (this.TryGetParrentOfType<GameRoot>(out var pGameRoot) && pGameRoot.ViewManager != null)
            {
                var pSimulation = pGameRoot.ViewManager.Simulation;
                var pHexGrid = new HexGrid(pSimulation, Width, Height);

                if (BaseNoise == null || BaseNoise.GetLength(0) != BaseWidth || BaseNoise.GetLength(1) != BaseHeight)
                {
                    BaseNoise = new float[BaseWidth, BaseHeight];

                    for (var iX = 0; iX < BaseWidth; iX++)
                    {
                        for (var iY = 0; iY < BaseHeight; iY++)
                        {
                            BaseNoise[iX, iY] = (float)Random.NextDouble();
                        }
                    }
                }

                var NoiseScale = new Vector2(Width, Height) * CubeCoordinateExt.SizeScaler;

                for (int iX = 0; iX < Width; iX++)
                {
                    for (int iY = 0; iY < Height; iY++)
                    {
                        var xCubeCoordinate = new OffsetCoordinate(iX, iY).CubeCoordinate;

                        pSimulation.Spawn(new Tile(pHexGrid, (int)(HeightMultipier * Sample(xCubeCoordinate.GetWorldCoordinates() / NoiseScale)), xCubeCoordinate));
                    }
                }

                var xStartUnitCoordinate = new OffsetCoordinate((int)(Random.NextDouble() * Width), (int)(Random.NextDouble() * Height)).CubeCoordinate;
                pSimulation.Spawn(new Unit(pHexGrid, xStartUnitCoordinate, null, 10.0));
            }
        }

        private float Sample(Vector2 xPosition)
        {
            var fResult = 0f;

            for (var iLevel = 0; iLevel < LevelCount; iLevel++)
            {
                fResult += SampleLevel(xPosition, iLevel);
            }

            return fResult;
        }

        private float SampleLevel(Vector2 xPosition, int iLevel)
        {
            var CoordinateMultiplier = (float)Math.Pow(2, iLevel);
            var LevelMultiplier = 1f / CoordinateMultiplier;

            var xScaledPosition = xPosition * CoordinateMultiplier * new Vector2(BaseWidth, BaseHeight);

            var iLeft = IntFract((int)Math.Floor(xScaledPosition.x), BaseWidth);
            var iRight = IntFract((int)Math.Ceiling(xScaledPosition.x), BaseWidth);
            var iDown = IntFract((int)Math.Floor(xScaledPosition.y), BaseHeight);
            var iUp = IntFract((int)Math.Ceiling(xScaledPosition.y), BaseHeight);

            if (iLeft >= BaseWidth || iRight >= BaseWidth || iDown >= BaseHeight || iUp >= BaseHeight)
            {
                GD.PushError("Error!");
            }

            var xInterpolation = new Vector2(Fract(xScaledPosition.x), Fract(xScaledPosition.y));

            return Lerp(
                Lerp(BaseNoise[iLeft, iDown], BaseNoise[iRight, iDown], xInterpolation.x),
                Lerp(BaseNoise[iLeft, iUp], BaseNoise[iRight, iUp], xInterpolation.x),
                xInterpolation.y
            ) * LevelMultiplier;
        }

        private float Lerp(float a, float b, float c)
        {
            return a + c * (b - a);
        }

        private float Fract(float a)
        {
            return a - (float)Math.Truncate(a);
        }

        private int IntFract(int a, int max)
        {
            return Math.Min(Math.Max((int)(Fract((float)a / (float)max) * max), 0), max);
        }
    }
}