
using TerraNova.HexGrids.Coordinates;

namespace TerraNova.HexGrids.Units
{
    public class Unit
    {
        public CubeCoordinate Coordinate { get; private set; }
        public HexGrid HexGrid { get; private set; }

        public Player Owner { get; private set; }
        public double ActionPoints { get; private set; }
    }
}