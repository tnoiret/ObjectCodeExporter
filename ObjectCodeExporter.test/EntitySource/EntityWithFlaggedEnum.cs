using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCodeExporter.Test.EntitySource
{
    public class EntityWithFlaggedEnum
    {
        public MyEnum MyProperty { get; set; }

        public MyEnum MyProperty2 { get; set; }

    }

    [Flags]
    public enum MyEnum
    {
        None,
        Value1,
        Value2,
        Value3,
        Value4,
        Value8
    }
}
