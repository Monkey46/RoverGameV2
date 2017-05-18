using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
	public class GUIElementConnect : GUIPopUpElement
	{
		public GUIElementConnect(float x, float y, float width, GameObject gameObj, GameGrid gamegrid) : base("Connect", x, y, width, gameObj, gamegrid)
		{
		}
		public override bool Action()
		{

			//new type of popup
			//list all batteries of selected rover + none
			return false;
		}
	}
}
