using System;
using Dojo;
using Dojo.Starknet;

namespace Amegakure.Verdania.DojoModels
{
    public class Game : ModelInstance
    {
        [ModelField("id")]
        public FieldElement gameID;
        [ModelField("index")]
        public UInt64 index;
    }
}
