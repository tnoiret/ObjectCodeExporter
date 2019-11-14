using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectLiteralVisualizer.Test.EntitySource
{
    public class EntityWithTwoLevelChildEntity
    {
        public ChildEntityLevel1 MyProperty { get; set; } = new ChildEntityLevel1();
    }

    public class ChildEntityLevel1
    {
        public int MyProperty { get; set; } = 10;

        public ChildEntityLevel2 MyProperty2 { get; set; } = new ChildEntityLevel2();
    }

    public class ChildEntityLevel2
    {
        public int MyProperty { get; set; } = 10;
    }
}
