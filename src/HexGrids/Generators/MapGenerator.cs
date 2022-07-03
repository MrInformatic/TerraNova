using Godot;
using TerraNova.HexGrids;
using TerraNova.HexGrids.Tiles;

namespace TerraNova.HexGrids.Generators
{
    public abstract class MapGenerator : Resource
    {
        public abstract HexStorage<Tile> GenerateMap(int iWidth, int iHeight);
    }
}