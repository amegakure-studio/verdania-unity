using System;
using Dojo;

namespace Amegakure.Verdania.DojoModels
{
    public enum TileType
    {
        None,
        Bridge,
        Building,
        Dirt,
        Grass,
        Mountain,
        Sand,
        Water,
    }

    public class Tile : ModelInstance
    {
        [ModelField("map_id")]
        public UInt64 mapID;

        [ModelField("id")]
        public UInt64 id;

        [ModelField("x")]
        public UInt64 x;

        [ModelField("y")]
        public UInt64 y;

        [ModelField("tile_type")]
        public TileType tileType;
    }
}



