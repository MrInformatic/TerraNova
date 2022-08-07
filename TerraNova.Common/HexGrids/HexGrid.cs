using TerraNova.Common.HexGrids.Tiles;
using TerraNova.Common.HexGrids.Units;
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
    }
}