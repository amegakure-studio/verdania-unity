using System;
using Dojo;
namespace Amegakure.Verdania.DojoModels
{
    public class Map : ModelInstance
    {
        [ModelField("id")]
        public UInt64 id;

        [ModelField("height")]
        public UInt64 height;

        [ModelField("width")]
        public UInt64 width;
    }
}
