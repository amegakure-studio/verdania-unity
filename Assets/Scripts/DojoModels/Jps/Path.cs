using Dojo;
using Dojo.Starknet;
using Dojo.Torii;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amegakure.Verdania.DojoModels
{
    public class Path : ModelInstance
    {
        [ModelField("player")]
        public FieldElement player;

        [ModelField("id")]
        public UInt64 id;

        [ModelField("x")]
        public UInt64 x;

        [ModelField("y")]
        public UInt64 y;

    }
}

