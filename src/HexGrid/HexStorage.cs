using System;
using System.Collections;
using System.Collections.Generic;

namespace TerraNova.Hexgrid
{
    public class HexStorage<T> : IEnumerable<(OffsetCoordinate, T)>
    {
        public int Count => Values.Length;
        public int Width => Values.GetLength(0);
        public int Height => Values.GetLength(1);

        private T[,] Values;

        public HexStorage(int iWidth, int iHeight)
        {
            this.Values = new T[iWidth, iHeight];
        }

        public T this[OffsetCoordinate xOffsetCoordinate]
        {
            get
            {
                return Values[xOffsetCoordinate.Col, xOffsetCoordinate.Row];
            }
            set
            {
                Values[xOffsetCoordinate.Col, xOffsetCoordinate.Row] = value;
            }
        }

        public IEnumerator<(OffsetCoordinate, T)> GetEnumerator()
        {
            for (var iY = 0; iY < Height; iY++)
            {
                for (var iX = 0; iX < Width; iX++)
                {
                    yield return (new OffsetCoordinate(iX, iY), Values[iX, iY]);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}