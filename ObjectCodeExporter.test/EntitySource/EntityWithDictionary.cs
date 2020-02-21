using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectCodeExporter.Test.EntitySource
{
    public class EntityWithDictionary
    {
        private Dictionary<string, string> _dico = new Dictionary<string, string>();

        public Dictionary<string, string> Dico
        {
            get { return _dico; }
            private set { _dico = value; }
        }

    }
}
