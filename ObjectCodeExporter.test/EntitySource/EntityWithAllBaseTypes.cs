using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCodeExporter.Test.EntitySource
{
    public class EntityWithAllBaseTypes
    {
        public object Mp1 { get; set; } = null;
        public string Mp2 { get; set; } = "Test Lol c\'est cool j\'ai même mis un \"";
        public int Mp3 { get; set; } = 42;
        public bool Mp4 { get; set; } = true;
        public short Mp5 { get; set; } = -2;
        public byte Mp6 { get; set; } = 0x42;
        public uint Mp7 { get; set; } = 25;
        public ushort Mp8 { get; set; } = 3;
        public sbyte Mp9 { get; set; } = 0x43;
        public double Mp10 { get; set; } = 25.6d;
        public decimal Mp11 { get; set; } = 26.5m;
        public float Mp12 { get; set; } = 62.5f;
        public long Mp13 { get; set; } = -1265465654L;
        public ulong Mp14 { get; set; } = 156456465L;
        public DateTime Mp15 { get; set; } = new DateTime(2017, 5, 27, 14, 32, 25, 122);
        public Guid Mp16 { get; set; } = new Guid("51326947-38ac-4300-a02c-ede7a9698a16");
    }
}
