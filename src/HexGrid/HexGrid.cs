using Godot;
using System;
using TerraNova.Hexgrid.Generator;
using TerraNova.Utils;
using System.Collections.Generic;

namespace TerraNova.Hexgrid
{
    public class HexGrid : Spatial
    {
        public class Tile
        {
            public int Height { get; set; }
            public RID Instance { get; set; }
        }

        public static readonly Basis TileTransform = new Basis()
        {
            Row0 = new Vector3((float)Math.Sin(Math.PI / 3f) * 2, (float)-Math.Sin(Math.PI / 3f), 0f),
            Row2 = new Vector3(0f, -1.5f, 0f),
            Row1 = new Vector3(0f, 0f, 0.1f),
        };

        [Export] public int Width { get; set; }
        [Export] public int Height { get; set; }
        [Export] public MapGenerator MapGenerator { get; set; }

        public HexStorage<Tile> Map { get; set; }

        [Export] public Mesh TileMesh;

        public override void _Ready()
        {
            Map = MapGenerator.GenerateMap(Width, Height);

            UpdateTiles();
        }

        public void UpdateTiles()
        {
            foreach (var (xCoordinate, pTile) in Map)
            {
                var xCubeCoordinate = xCoordinate.CubeCoordinate;
                var xWorldCoordinates = xCubeCoordinate.WorldCoordinates;

                if (pTile.Instance == null)
                {
                    var pInstance = VisualServer.InstanceCreate2(new RID(TileMesh), GetWorld().Scenario);

                    pTile.Instance = pInstance;
                }

                VisualServer.InstanceSetTransform(pTile.Instance, new Transform(Basis.Identity, new Vector3(xWorldCoordinates.x, pTile.Height * 0.1f, xWorldCoordinates.y)));
            }
        }
    }
}
