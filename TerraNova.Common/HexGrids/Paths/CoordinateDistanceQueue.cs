using TerraNova.Common.HexGrids.Coordinates;
using TerraNova.Common.Utils;
using System.Collections.Generic;

namespace TerraNova.Common.HexGrids.Paths
{
    public class ClosedListComparer : IComparer<CubeCoordinate>
    {
        public Dictionary<CubeCoordinate, AStarCell> ClosedList { get; }

        public ClosedListComparer(Dictionary<CubeCoordinate, AStarCell> pClosedList)
        {
            ClosedList = pClosedList;
        }

        public int Compare(CubeCoordinate x, CubeCoordinate y)
        {
            var DistanceX = ClosedList.TryGetValue(x, out var CellX) ? CellX.Distance : int.MaxValue;
            var DistanceY = ClosedList.TryGetValue(y, out var CellY) ? CellY.Distance : int.MaxValue;

            return DistanceX.CompareTo(DistanceY);
        }
    }

    public class CoordinateDistanceQueue
    {
        public List<CubeCoordinate> PriorityQueue { get; }

        public CoordinateDistanceQueue()
        {
            PriorityQueue = new List<CubeCoordinate>();
        }

        public bool Add(CubeCoordinate xCoordinate)
        {
            var bAdd = !PriorityQueue.Contains(xCoordinate);

            if (bAdd)
            {
                PriorityQueue.Add(xCoordinate);
            }

            return bAdd;
        }

        public bool TryPop(IComparer<CubeCoordinate> pComparer, out CubeCoordinate xCoordinate)
        {
            var bPop = false;
            xCoordinate = default;

            foreach (var xCurrent in PriorityQueue)
            {
                if (!bPop || pComparer.Compare(xCurrent, xCoordinate) < 0)
                {
                    bPop = true;
                    xCoordinate = xCurrent;
                }
            }

            return bPop;
        }
    }
}