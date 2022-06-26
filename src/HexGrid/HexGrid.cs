using Godot;
using System;
using TerraNova.Hexgrid.Generator;
using TerraNova.Utils;
using TerraNova.Loading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace TerraNova.Hexgrid
{
    public class HexGrid : Spatial, IProgressProvider
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

        public double Progress
        {
            get
            {
                return (double)LoadedTiles / (double)this.Map.Count;
            }
        }
        private int LoadedTiles = 0;

        public bool IsFinished { get; private set; } = false;

        [Export] public Mesh TileMesh { get; set; }
        [Export] public PackedScene TileScene { get; set; }

        private SemaphoreSlim UpdateLock { get; } = new SemaphoreSlim(1, 1);

        public override async void _Ready()
        {
            if (this.TryGetParrentOfType<GameState>(out var pGameState))
            {
                pGameState.ProgressProvider.Register(this);
            }

            Map = MapGenerator.GenerateMap(Width, Height);

            await UpdateTiles();
        }

        public async Task UpdateTiles()
        {
            await UpdateLock.WaitAsync();

            this.IsFinished = false;
            this.LoadedTiles = 0;

            var pTaskFactory = new TaskFactory(GodotTaskScheduler.Current);

            await pTaskFactory.StartNew(() =>
                {
                    foreach (var (xCoordinate, pTile) in Map)
                    {
                        var xCubeCoordinate = xCoordinate.CubeCoordinate;
                        var xWorldCoordinates = xCubeCoordinate.WorldCoordinates;

                        var pTileNode = TileScene.Instance<Spatial>();

                        pTileNode.Translation = new Vector3(xWorldCoordinates.x, pTile.Height * 0.1f, xWorldCoordinates.y);

                        CallDeferred("add_child", pTileNode);

                        Interlocked.Increment(ref LoadedTiles);
                    }
                });

            this.IsFinished = true;

            UpdateLock.Release();
        }
    }
}
