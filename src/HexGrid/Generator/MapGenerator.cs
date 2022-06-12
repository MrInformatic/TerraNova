using Godot;
using TerraNova.Hexgrid;

namespace TerraNova.Hexgrid.Generator
{
    public abstract class MapGenerator : Resource
    {
        public abstract HexStorage<HexGrid.Tile> GenerateMap(int iWidth, int iHeight);
    }
}