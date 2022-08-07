
using TerraNova.Common.HexGrids.Coordinates;

namespace TerraNova.Common.HexGrids.Units
{
    public class Unit : SimulationObject
    {
        public HexGrid HexGrid { get; private set; }
        public CubeCoordinate Coordinate { get; private set; }

        public Player Owner { get; private set; }
        public double ActionPoints { get; private set; }

        protected override bool CanSpawn => HexGrid.CanAddUnit(this);

        public Unit(HexGrid pHexGrid, CubeCoordinate xCoordinate, Player pOwner, double fActionPoints)
        {
            HexGrid = pHexGrid;
            Coordinate = xCoordinate;
            Owner = pOwner;
            ActionPoints = fActionPoints;
        }

        protected override void OnSpawn()
        {
            HexGrid.AddUnit(this);
        }

        protected override void OnDeSpawn()
        {
            HexGrid.RemoveUnit(this);
        }
    }
}