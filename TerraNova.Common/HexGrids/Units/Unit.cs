
using System;
using TerraNova.Common.HexGrids.Coordinates;
using TerraNova.Common.HexGrids.Tiles;

namespace TerraNova.Common.HexGrids.Units
{
    public class Unit : SimulationObject
    {
        public Guid TileId { get; private set; }
        public Tile Tile
        {
            get
            {
                return GetSimulationObject<Tile>(TileId);
            }
        }
        public Player Owner { get; private set; }
        public double ActionPoints { get; private set; }

        public Unit(Guid xTileId, Player pOwner, double fActionPoints)
        {
            TileId = xTileId;
            Owner = pOwner;
            ActionPoints = fActionPoints;
        }

        protected override bool CanSpawn => Tile?.CanAddUnit(this) ?? false;

        protected override void OnSpawn()
        {
            Tile?.AddUnit(this);
        }

        protected override void OnDeSpawn()
        {
            Tile?.RemoveUnit(this);
        }

        internal bool CanTeleport(Tile pTile)
        {
            return pTile != null && pTile.IsValid;
        }

        internal void Teleport(Tile pTile)
        {
            if (CanTeleport(pTile))
            {
                TileId = pTile.Guid;
            }
        }
    }
}