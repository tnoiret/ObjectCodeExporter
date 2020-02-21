using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCodeExporter.Test.EntitySource
{
    public class EntityWithListOfChild
    {
        private List<Child> _childs = new List<Child>();

        public List<Child> Childs
        {
            get { return _childs;  }
            private set { _childs = value; }
        }

        public int MyIntToo { get; set; }
    }

    public class Child
    {
        public int MyInt { get; set; }
    }
}
