using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
	public class GUIElementDrop : GUIPopUpElement
	{
		public GUIElementDrop(float x, float y, float width, GameObject gameObj, GameGrid gamegrid) : base("Drop", x, y, width, gameObj, gamegrid)
		{

		}

		public override void Action()
		{
			// Drop Game object
			GameGrid.Drop(GameObj);
			// RemovePanel
			//removePanel = iGUIPanel;
		}
	}
}
