using TerraNova.Common.HexGrids.Coordinates;

namespace TerraNova.Common.HexGrids.Paths
{
    public struct AStarCell
    {
        public CubeCoordinate CameFrom { get; private set; }
        public int DistanceFromStart { get; private set; }
        public int DistanceToDesination { get; private set; }
        public bool StartCell { get; private set; }
        public int Distance => DistanceFromStart + DistanceToDesination;

        public static AStarCell CreateCell(CubeCoordinate xCameFrom, int iDistanceFromStart, int iDistanceToDesination)
        {
            return new AStarCell()
            {
                CameFrom = xCameFrom,
                DistanceFromStart = iDistanceFromStart,
                DistanceToDesination = iDistanceToDesination,
                StartCell = false,
            };
        }

        public static AStarCell CreateStartCell(int iDistanceToDesination)
        {
            return new AStarCell()
            {
                DistanceFromStart = 0,
                DistanceToDesination = iDistanceToDesination,
                StartCell = true,
            };
        }
    }
}