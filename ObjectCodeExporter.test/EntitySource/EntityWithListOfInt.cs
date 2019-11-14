using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectLiteralVisualizer.Test.EntitySource
{
    public class EntityWithListOfInt
    {
        public int MyInt { get; set; }

        private List<int> _myIntList = new List<int>();

        public List<int> MyIntList { get { return _myIntList; } private set{ _myIntList = value; } }
    }
}
