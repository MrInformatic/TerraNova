using Godot;
using Godot.Collections;
using System.Text.RegularExpressions;
using System;
using TerraNova.HexGrids.Tiles;
using TerraNova.HexGrids.Coordinates;

namespace TerraNova.HexGrids.Generators
{
    public class GausianNoiseMapGenerator : MapGenerator
    {
        public static readonly Regex LevelsRegex = new Regex("^([0-9]+)/([A-Za-z]+)$");
        public static readonly Random Random = new Random();

        public struct GausianNoiseLevel
        {
            public float LevelMultiplier;
            public float CoordinateMultiplier;
        }

        [Export] public int BaseWidth { get; set; }
        [Export] public int BaseHeight { get; set; }

        [Export] public int LevelCount { get; set; }
        [Export] public float HeightMultipier { get; set; }

        private float[,] BaseNoise;

        public override HexStorage<Tile> GenerateMap(int iWidth, int iHeight)
        {
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

            var pMap = new HexStorage<Tile>(iWidth, iHeight);
            var NoiseScale = new Vector2(iWidth, iHeight) * CubeCoordinate.SizeScaler;

            foreach (var (xCoordinate, _) in pMap)
            {
                var xCubeCoordinate = xCoordinate.CubeCoordinate;

                pMap[xCoordinate] = new Tile()
                {
                    Height = (int)(HeightMultipier * Sample(xCubeCoordinate.WorldCoordinates / NoiseScale)),
                };
            }

            return pMap;
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