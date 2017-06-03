using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
	/// <summary>
	/// This GUI pop-up element will repair the drill
	/// </summary>
	public class GUIElementRepair : GUIPopUpElement
	{
		public GUIElementRepair( float x, float y, float width, GameObject gameObj, GameGrid gamegrid) : base("Repair", x, y, width, gameObj, gamegrid)
		{

		}

		public override GUIPopUp Action()
		{
			(GameObj as Drill).Worn = 0;
			return null;
		}
	}
}
