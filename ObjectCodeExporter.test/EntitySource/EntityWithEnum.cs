using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCodeExporter.Test.EntitySource
{
    public class EntityWithEnum
    {
        public MyEnumV2 MyProperty { get; set; }

    }

    public enum MyEnumV2
    {
        Value1,
        Value2,
        Value3,
        Value4,
        Value8
    }
}
