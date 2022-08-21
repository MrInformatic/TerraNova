using System;
using System.Collections.Generic;
using TerraNova.Common.Utils;

namespace TerraNova.Common.HexGrids.Coordinates
{
    public struct CubeCoordinate : IEquatable<CubeCoordinate>
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

        public int Distance(CubeCoordinate xOther)
        {
            return (Math.Abs(X - xOther.X) + Math.Abs(Y - xOther.Y) + Math.Abs(Z - xOther.Z)) / 2;
        }

        public IEnumerable<CubeCoordinate> Neighbours()
        {
            yield return new CubeCoordinate()
            {
                X = X + 1,
                Y = Y - 1,
                Z = Z
            };

            yield return new CubeCoordinate()
            {
                X = X - 1,
                Y = Y + 1,
                Z = Z
            };

            yield return new CubeCoordinate()
            {
                X = X + 1,
                Y = Y,
                Z = Z - 1
            };

            yield return new CubeCoordinate()
            {
                X = X - 1,
                Y = Y,
                Z = Z + 1
            };

            yield return new CubeCoordinate()
            {
                X = X,
                Y = Y + 1,
                Z = Z - 1
            };

            yield return new CubeCoordinate()
            {
                X = X,
                Y = Y - 1,
                Z = Z + 1
            };
        }

        public bool Equals(CubeCoordinate xOther)
        {
            return X == xOther.X && Y == xOther.Y && Z == xOther.Z;
        }

        public override int GetHashCode()
        {
            return HashCodeBuilder.Create()
                .Add(X)
                .Add(Y)
                .Add(Z)
                .GetHashCode();
        }
    }
}