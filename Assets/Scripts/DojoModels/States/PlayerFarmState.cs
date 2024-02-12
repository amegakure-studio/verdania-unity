using Dojo.Starknet;
using Dojo.Torii;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dojo
{
    public class PlayerFarmState : ModelInstance
    {
        [ModelField("player")]
        public FieldElement player_id;

        [ModelField("map_id")]
        public UInt64 map_id;

        [ModelField("id")]
        public UInt64 farm_id;

        [ModelField("name")]
        public FieldElement player_name;

        [ModelField("crops_len")]
        public UInt64 crops_len;

        [ModelField("env_entities_len")]
        public UInt64 env_entities_len;

        [ModelField("connected_players")]
        public UInt64 connected_players;

        [ModelField("open")]
        public bool open;

        [ModelField("invitation_code")]
        public FieldElement invitation_code;

    }
}
