using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
	/// <summary>
	/// Cell has a list of Game objects that are in it
    /// this class is currently redundant but will be used later on for better collision detection
	/// </summary>
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
