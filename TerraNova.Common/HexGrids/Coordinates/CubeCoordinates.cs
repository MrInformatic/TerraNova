using System;

namespace TerraNova.Common.HexGrids.Coordinates
{
    public struct CubeCoordinate
    {
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