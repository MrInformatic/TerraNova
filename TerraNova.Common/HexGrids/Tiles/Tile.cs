using TerraNova.Common.HexGrids.Coordinates;
using TerraNova.Common.HexGrids.Units;

namespace TerraNova.Common.HexGrids.Tiles
{
    public class Tile : SimulationObject
    {
        public HexGrid HexGrid { get; }
        public int Height { get; }
        public CubeCoordinate Coordinate { get; }

        public Unit Unit { get; private set; } = null;

        public Tile(HexGrid pHexGrid, int iHeight, CubeCoordinate xCoordinate)
        {
            HexGrid = pHexGrid;
            Height = iHeight;
            Coordinate = xCoordinate;
        }

        protected override bool CanSpawn => HexGrid.CanAddTile(this);

        protected override void OnSpawn()
        {
            HexGrid.AddTile(this);
        }

        protected override void OnDeSpawn()
        {
            HexGrid.RemoveTile(this);
        }

        internal bool CanAddUnit(Unit pUnit)
        {
            return Unit == null && pUnit != null;
        }

        internal void AddUnit(Unit pUnit)
        {
            if (CanAddUnit(pUnit))
            {
                Unit = pUnit;
            }
        }

        internal void RemoveUnit(Unit pUnit)
        {
            if (Unit != null && Unit == pUnit)
            {
                Unit = null;
            }
        }
    }
}