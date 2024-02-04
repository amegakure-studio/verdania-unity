using System;
using Dojo;
namespace Amegakure.Verdania.DojoModels
{
    public class WorldConfig : ModelInstance
    {
        [ModelField("id")]
        public UInt64 gameID;
        
        [ModelField("weather")]
        public byte weather;

        [ModelField("sync_time")]
        public UInt64 sync_time;

        [ModelField("max_players_per_farm")]
        public byte max_players_per_farm;

        [ModelField("active_players_len")]
        public UInt64 active_players_len;

        [ModelField("max_time_unactivity")]
        public UInt64 max_time_unactivity;
        
        [ModelField("market_close_time")]
        public UInt64 market_close_time;

        [ModelField("market_open_time")]
        public UInt64 market_open_time;
    }   
}
