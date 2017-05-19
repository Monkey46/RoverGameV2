using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
	public class GUIElementBattery : GUIPopUpElement
	{
		Device _device;
		Battery _battery;
		public GUIElementBattery(string name, float x, float y, float width, GameObject device, Battery battery, GameGrid gamegrid) : base(name, x, y, width, device, gamegrid)
		{
			_device = device as Device;
			_battery = battery;
		}

		public override GUIPopUp Action()
		{
			_device.ConnectedBattery = _battery;
			return null;
		}
	}
}
