using Godot;
using System;

namespace TerraNova.Hexgrid
{
    public struct CubeCoordinate
    {
        public static readonly Vector2 TransformX = new Vector2((float)Math.Sin(Math.PI / 3f) * 2, 0f);
        public static readonly Vector2 TransformZ = new Vector2((float)-Math.Sin(Math.PI / 3f), -1.5f);
        public static readonly Vector2 SizeScaler = new Vector2((float)Math.Sin(Math.PI / 3f) * 2, -1.5f);

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        public OffsetCoordinate OffsetCoordinate
        {
            get
            {
                return new OffsetCoordinate()
                {
                    Col = X - Z / 2,
                    Row = Z,
                };
            }
        }

        public Vector2 WorldCoordinates
        {
            get
            {
                return TransformX * X + TransformZ * Z;
            }
        }

        public static bool TryGetCubeCoordinate(int iX, int iY, int iZ, out CubeCoordinate xCubeCoordinate)
        {
            if (iX + iY + iZ == 0)
            {
                xCubeCoordinate = new CubeCoordinate()
                {
                    X = iX,
                    Y = iY,
                    Z = iZ,
                };

                return true;
            }

            xCubeCoordinate = new CubeCoordinate();
            return false;
        }

        public static CubeCoordinate FromAxialXY(int iX, int iY)
        {
            return new CubeCoordinate
            {
                X = iX,
                Y = iY,
                Z = -iX - iY
            };
        }

        public static CubeCoordinate FromAxialXZ(int iX, int iZ)
        {
            return new CubeCoordinate
            {
                X = iX,
                Y = -iX - iZ,
                Z = iZ,
            };
        }

        public static CubeCoordinate FromAxialYZ(int iY, int iZ)
        {
            return new CubeCoordinate
            {
                X = -iY - iZ,
                Y = iY,
                Z = iZ
            };
        }
    }
}