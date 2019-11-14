using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectLiteralVisualizer.Test.EntitySource
{
    public class test
    {
        List<List<OtherChild>> _toto = new List<List<OtherChild>>();

        public List<List<OtherChild>> Toto
        {
            get { return _toto; }
            private set { _toto = value; }
        }
    }

    public class EntityWithListOfChildWithListOfOtherChild
    {
        private List<ChildWithListOfOtherChild> _childs = new List<ChildWithListOfOtherChild>();

        public List<ChildWithListOfOtherChild> Childs
        {
            get { return _childs;  }
            private set { _childs = value; }
        }

        public int MyIntToo { get; set; }
    }

    public class ChildWithListOfOtherChild
    {
        public int MyInt { get; set; }
        List<OtherChild> _otherChilds = new List<OtherChild>();

        public List<OtherChild> OtherChilds
        {
            get { return _otherChilds; }
            private set { _otherChilds = value; }
        }
    }

    public class OtherChild
    {
        public int MyIntAgain { get; set; }

    }
}
