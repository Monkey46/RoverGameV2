using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
    public class Cell
    {
        List<GameObject> _contents;

        public Cell()
        {
            _contents = new List<GameObject>();
        }
        public List<GameObject> Contents
        {
            get { return _contents; }
        }
    }
}
