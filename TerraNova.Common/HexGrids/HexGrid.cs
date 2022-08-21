using TerraNova.Common.HexGrids.Tiles;
using TerraNova.Common.HexGrids.Units;
using TerraNova.Common.HexGrids.Coordinates;
using TerraNova.Common.HexGrids.Paths;
using System.Collections.Generic;
using System;

namespace TerraNova.Common.HexGrids
{
    public class HexGrid
    {
        public HexStorage<Tile> Map { get; }
        public Simulation Simulation { get; }

        public HexGrid(Simulation Simulation, int iWidth, int iHeight)
        {
            Map = new HexStorage<Tile>(iWidth, iHeight);

            foreach (var pSimulationObject in Simulation.SimulationObjects.Values)
            {
                if (pSimulationObject is Tile pTile)
                {
                    if (CanAddTile(pTile))
                    {
                        AddTile(pTile);
                    }
                    else
                    {
                        Simulation.DeSpawn(pTile);
                    }
                }

                if (pSimulationObject is Unit pUnit)
                {
                    if (!CanAddUnit(pUnit))
                    {
                        Simulation.DeSpawn(pUnit);
                    }
                }
            }
        }

        internal bool CanAddTile(Tile pTile)
        {
            return pTile != null && Map[pTile.Coordinate.OffsetCoordinate] == null;
        }

        internal void AddTile(Tile pTile)
        {
            if (CanAddTile(pTile))
            {
                Map[pTile.Coordinate.OffsetCoordinate] = pTile;
            }
        }

        internal void RemoveTile(Tile pTile)
        {
            if (pTile != null && Map[pTile.Coordinate.OffsetCoordinate] != null)
            {
                Map[pTile.Coordinate.OffsetCoordinate] = null;
            }
        }

        internal bool CanAddUnit(Unit pUnit)
        {
            var pTile = Map[pUnit.Coordinate.OffsetCoordinate];
            return pTile != null && pTile.CanAddUnit(pUnit);
        }

        internal void AddUnit(Unit pUnit)
        {
            var pTile = Map[pUnit.Coordinate.OffsetCoordinate];

            if (pTile != null && pTile.CanAddUnit(pUnit))
            {
                pTile.AddUnit(pUnit);
            }
        }

        internal void RemoveUnit(Unit pUnit)
        {
            var pTile = Map[pUnit.Coordinate.OffsetCoordinate];

            if (pTile != null)
            {
                pTile.RemoveUnit(pUnit);
            }
        }

        public bool UnitPath(Unit pUnit, CubeCoordinate xDestination, ref List<CubeCoordinate> pPath)
        {
            var pOpenList = new CoordinateDistanceQueue();
            var pClosedList = new Dictionary<CubeCoordinate, AStarCell>();
            var pComparer = new ClosedListComparer(pClosedList);

            pOpenList.Add(pUnit.Coordinate);

            pPath.Clear();

            while (pOpenList.TryPop(pComparer, out var xCurrent))
            {
                if (xCurrent.Equals(xDestination))
                {
                    pPath.Add(xCurrent);
                    while (pClosedList.TryGetValue(xCurrent, out var xCell))
                    {
                        xCurrent = xCell.CameFrom;
                        pPath.Add(xCurrent);
                    }
                    pPath.Reverse();

                    return true;
                }

                foreach (var xNeighbour in xCurrent.Neighbours())
                {
                    var iDistanceFromStart = pClosedList[xCurrent].DistanceFromStart + 1;
                    if (iDistanceFromStart < pClosedList[xNeighbour].DistanceFromStart)
                    {
                        pClosedList[xNeighbour] = new AStarCell(xCurrent, iDistanceFromStart, xNeighbour.Distance(xDestination));

                        pOpenList.Add(xNeighbour);
                    }

                }
            }

            return false;
        }

        public void MoveUnit(Unit pUnit, CubeCoordinate xDestination)
        {

        }
    }
}