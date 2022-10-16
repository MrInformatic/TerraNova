using System;
using TerraNova.Common.HexGrids.Coordinates;
using TerraNova.Common.HexGrids.Units;

namespace TerraNova.Common.HexGrids.Tiles
{
    public class Tile : SimulationObject
    {
        public Guid HexGridId { get; }
        public HexGrid HexGrid
        {
            get
            {
                return GetSimulationObject<HexGrid>(HexGridId);
            }
        }

        public int Height { get; }
        public CubeCoordinate Coordinate { get; }

        public Guid UnitId { get; private set; }
        public Unit Unit
        {
            get
            {
                return GetSimulationObject<Unit>(UnitId);
            }
        }

        public Tile(Guid xHexGridId, int iHeight, CubeCoordinate xCoordinate)
        {
            HexGridId = xHexGridId;
            Height = iHeight;
            Coordinate = xCoordinate;
        }

        protected override bool CanSpawn => HexGrid?.CanAddTile(this) ?? false;

        protected override void OnSpawn()
        {
            HexGrid?.AddTile(this);
        }

        protected override void OnDeSpawn()
        {
            HexGrid?.RemoveTile(this);
        }

        internal bool CanAddUnit(Unit pUnit)
        {
            return Unit == null && pUnit != null;
        }

        internal void AddUnit(Unit pUnit)
        {
            if (this.CanAddUnit(pUnit))
            {
                UnitId = pUnit.Guid;
            }
        }

        internal void RemoveUnit(Unit pUnit)
        {
            if (pUnit != null && pUnit.Guid != Guid.Empty && pUnit.Guid == UnitId)
            {
                UnitId = Guid.Empty;
            }
        }
    }
}