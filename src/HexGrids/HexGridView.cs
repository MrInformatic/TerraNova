using Godot;
using System;
using TerraNova.HexGrids.Generators;
using TerraNova.HexGrids.Tiles;
using TerraNova.Utils;
using TerraNova.Loading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace TerraNova.HexGrids
{
    public class HexGridView : Spatial, IProgressProvider
    {
        public static readonly Basis TileTransform = new Basis()
        {
            Row0 = new Vector3((float)Math.Sin(Math.PI / 3f) * 2, (float)-Math.Sin(Math.PI / 3f), 0f),
            Row2 = new Vector3(0f, -1.5f, 0f),
            Row1 = new Vector3(0f, 0f, 0.1f),
        };

        [Export] public int Width { get; set; }
        [Export] public int Height { get; set; }
        [Export] public MapGenerator MapGenerator { get; set; }
        public HexGrid HexGrid { get; private set; }

        public double Progress
        {
            get
            {
                return HexGrid != null ? (double)LoadedTiles / (double)HexGrid.Map.Count : 0.0;
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

            this.HexGrid = new HexGrid()
            {
                Map = MapGenerator.GenerateMap(Width, Height)
            };

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
                    foreach (var (xCoordinate, pTile) in this.HexGrid.Map)
                    {
                        var pTileNode = TileScene.Instance<TileView>();

                        pTileNode.Tile = pTile;
                        pTileNode.Coordinate = xCoordinate.CubeCoordinate;
                        pTileNode.UpdateView();

                        CallDeferred("add_child", pTileNode);

                        Interlocked.Increment(ref LoadedTiles);
                    }
                });

            this.IsFinished = true;

            UpdateLock.Release();
        }
    }
}
