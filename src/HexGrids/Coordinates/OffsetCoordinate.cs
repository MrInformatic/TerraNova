namespace TerraNova.HexGrids.Coordinates
{
    public struct OffsetCoordinate
    {
        public int Col { get; set; }
        public int Row { get; set; }

        public CubeCoordinate CubeCoordinate
        {
            get
            {
                return CubeCoordinate.FromAxialXZ(Col + Row / 2, Row);
            }
        }

        public OffsetCoordinate(int iX, int iY)
        {
            this.Col = iX;
            this.Row = iY;
        }
    }
}