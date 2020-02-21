﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCodeExporter.Test.EntitySource
{
    public class EntityWithChildEntity
    {
        public ChildEntity MyProperty { get; set; } = new ChildEntity();
    }

    public class ChildEntity
    {
        public int MyProperty { get; set; } = 10;
    }
}
