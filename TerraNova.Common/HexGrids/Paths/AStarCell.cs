using TerraNova.Common.HexGrids.Coordinates;

namespace TerraNova.Common.HexGrids.Paths
{
    public struct AStarCell
    {
        public CubeCoordinate CameFrom { get; }
        public int DistanceFromStart { get; private set; }
        public int DistanceToDesination { get; }
        public int Distance => DistanceFromStart + DistanceToDesination;

        public AStarCell(CubeCoordinate xCameFrom, int iDistanceFromStart, int iDistanceToDesination)
        {
            CameFrom = xCameFrom;
            DistanceFromStart = iDistanceFromStart;
            DistanceToDesination = iDistanceToDesination;
        }
    }
}