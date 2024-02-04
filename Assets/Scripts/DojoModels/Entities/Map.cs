using System;
using Dojo;
namespace Amegakure.Verdania.DojoModels
{
    public class Map : ModelInstance
    {
        [ModelField("id")]
        public UInt64 id;

        [ModelField("height")]
        public byte height;

        [ModelField("width")]
        public byte width;
    }
}
