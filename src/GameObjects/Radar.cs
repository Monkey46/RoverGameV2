using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// Raider is a device that scans the area and then renders green dots 
	/// </summary>
	public class Radar : Device
	{
		/// <summary>
		/// This is the type of object the radar scans for 
		/// </summary>
		private Type _type;
		private int _range;
		public Radar(string name, Type type, int range, GameGrid gamegrind) : this(name, 6, 4, type, range, gamegrind)
		{

		}
		public Radar(string name, float width, float height, Type type, int range, GameGrid gamegrind) : base(name, width, height, gamegrind)
		{
			_type = type;
			_range = range;
		}
		public Type Type
		{
			get { return _type; }
		}
		/// <summary>
		/// Scan the area then adds all the found GameObjects of type to scanned objects list in level 
		/// </summary>
		/// <returns></returns>
		public override bool Operate()
		{
			if (!CheckCanOperate(-4))
			{
				return false;
			}

			// Make Scan area 
			Circle scanArea = new Circle();
			scanArea.Center = (Owner as Rover).Center;
			scanArea.Radius = GameGrid.CellSize * _range;

			List<GameObject> scanedGOs = GameGrid.GetScannedGameObjects(scanArea);

			// Of all the GameObjects that are of scan Type  Add two levels scan list 
			foreach (GameObject scanGO in scanedGOs.FindAll(x => x.GetType() == _type))
			{
				if (scanGO != Owner)
				{
					GameGrid.Level.AddScanObeject(scanGO.Center);
				}
			}

			return true;
		}
		public override string Details()
		{
			return "Range: " + _range.ToString() + " Scan Type: " + _type.Name;
		}
		public override List<string> AllDetails()
		{
			List<string> allDetails = new List<string>();
			allDetails.Add(Name);
			allDetails.Add("Conected to " + ConnectedBatteryName);
			allDetails.Add("Scan Type: " + _type.Name);
			allDetails.Add("Range: " + _range);
			allDetails.Add("Decsription: Blah Blah Balh");
			return allDetails;
		}
		public override void Render()
		{
			SwinGame.FillRectangle(Color.DeepPink, X, Y, Width, Height);
		}
	}
}
