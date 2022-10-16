using TerraNova.Common.HexGrids.Tiles;
using TerraNova.Common.HexGrids.Units;
using TerraNova.Common.HexGrids.Coordinates;
using TerraNova.Common.HexGrids.Paths;
using System.Collections.Generic;
using System;

namespace TerraNova.Common.HexGrids
{
    public class HexGrid : SimulationObject
    {
        public HexStorage<Guid> Map { get; }

        public HexGrid(int iWidth, int iHeight)
        {
            Map = new HexStorage<Guid>(iWidth, iHeight);
        }

        internal bool CanAddTile(Tile pTile)
        {
            return pTile != null && GetSimulationObject<Tile>(Map[pTile.Coordinate.OffsetCoordinate]) == null;
        }

        internal void AddTile(Tile pTile)
        {
            if (CanAddTile(pTile))
            {
                Map[pTile.Coordinate.OffsetCoordinate] = pTile.Guid;
            }
        }

        internal void RemoveTile(Tile pTile)
        {
            if (pTile == null || pTile.Guid == Guid.Empty)
            {
                return;
            }

            var xCoordinate = pTile.Coordinate.OffsetCoordinate;
            if (Map[xCoordinate] == pTile.Guid)
            {
                Map[xCoordinate] = Guid.Empty;
            }
        }

        public bool UnitPath(CubeCoordinate xStart, CubeCoordinate xDestination, ref List<CubeCoordinate> pPath)
        {
            var pOpenList = new CoordinateDistanceQueue();
            var pClosedList = new Dictionary<CubeCoordinate, AStarCell>();
            var pComparer = new ClosedListComparer(pClosedList);

            pOpenList.Add(xStart);
            pClosedList.Add(xStart, AStarCell.CreateStartCell(xStart.Distance(xDestination)));

            pPath.Clear();

            while (pOpenList.TryPop(pComparer, out var xCurrent))
            {
                if (xCurrent.Equals(xDestination))
                {
                    pPath.Add(xCurrent);
                    while (pClosedList.TryGetValue(xCurrent, out var xCell) && !xCell.StartCell)
                    {
                        xCurrent = xCell.CameFrom;
                        pPath.Add(xCurrent);
                    }
                    pPath.Reverse();

                    return true;
                }

                foreach (var xNeighbour in xCurrent.Neighbours())
                {
                    var iDistanceFromStart = int.MaxValue;
                    if (pClosedList.TryGetValue(xCurrent, out var xCurrentCell))
                    {
                        iDistanceFromStart = xCurrentCell.DistanceFromStart + 1;
                    }

                    if (!pClosedList.TryGetValue(xNeighbour, out var xNeighbourCell) || iDistanceFromStart < xNeighbourCell.DistanceFromStart)
                    {
                        xNeighbourCell = AStarCell.CreateCell(xCurrent, iDistanceFromStart, xNeighbour.Distance(xDestination));
                        pClosedList[xNeighbour] = xNeighbourCell;
                        pOpenList.Add(xNeighbour);
                    }
                }
            }

            return false;
        }

        public bool MoveUnit(Unit pUnit, CubeCoordinate xDestination, ref List<CubeCoordinate> pPath)
        {
            if (pUnit == null || !pUnit.IsValid)
            {
                return false;
            }

            var pStartTile = pUnit.Tile;

            if (pStartTile == null || !this.UnitPath(pStartTile.Coordinate, xDestination, ref pPath) || pPath.Count == 0)
            {
                return false;
            }

            xDestination = pPath[pPath.Count - 1];
            var pDestinationTile = GetSimulationObject(Map[xDestination.OffsetCoordinate]) as Tile;
            if (!pDestinationTile.CanAddUnit(pUnit) && pUnit.CanTeleport(pDestinationTile))
            {
                return false;
            }

            pStartTile.RemoveUnit(pUnit);
            pDestinationTile.AddUnit(pUnit);
            pUnit.Teleport(pDestinationTile);
            return true;
        }
    }
}