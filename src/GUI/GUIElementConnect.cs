using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class GUIElementConnect : GUIPopUpElement
	{
		public GUIElementConnect(float x, float y, float width, GameObject gameObj, GameGrid gamegrid) : base("Connect", x, y, width, gameObj, gamegrid)
		{
		}
		public override GUIPopUp Action()
		{
			List<GUIPopUpElement> connectBatteries = new List<GUIPopUpElement>();
			GUIElementBattery none = new GUIElementBattery("none", SwinGame.MousePosition().X, SwinGame.MousePosition().Y, 96, GameObj, null, GameGrid);
			connectBatteries.Add(none);
			float spacing = 20;
			foreach (Battery bat in GameGrid.SelectedRover.Batteries)
			{
				GUIElementBattery conbat = new GUIElementBattery(bat.Name, SwinGame.MousePosition().X, SwinGame.MousePosition().Y + spacing, 96, GameObj, bat, GameGrid);
				connectBatteries.Add(conbat);
				spacing += 20;
			}
			GUIPopUp connectPopUp = new GUIPopUp(SwinGame.MousePosition(), GameObj, GameGrid, connectBatteries);
			//new type of popup
			//list all batteries of selected rover + none
			// @Task need to add this popup to the GUI render list

			return connectPopUp;
		}
	}
}
