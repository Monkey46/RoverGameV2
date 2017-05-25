using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class Camera : Device
	{
		private float _range;
		public Camera(string name, float range, GameGrid gamegrid) : base(name, gamegrid)
		{
			_range = range;
		}
		public Camera(string name, float width, float height, float range, GameGrid gamegrid) : base(name, width, height, gamegrid)
		{
			_range = range;
		}
		public override bool Operate()
		{
			if (!CheckCanOperate(-1))
			{
				GameGrid.Level.RenderList = null;
				return false;
			}

			Circle ViewArea = new Circle();
			ViewArea.Center = (Owner as Rover).Center;
			ViewArea.Radius = _range * GameGrid.CellSize;

			List<GameObject> viewedGameObjects = GameGrid.GetScannedGameObjects(ViewArea);

			GameGrid.Level.RenderList = viewedGameObjects;

			return true;

		}
		public override string Details()
		{
			return "Range " + _range.ToString();
		}
		public override List<string> AllDetails()
		{
			List<string> allDetails = new List<string>();
			allDetails.Add(Name);
			allDetails.Add("Conected to " + ConnectedBatteryName);
			allDetails.Add("Camera Range: " + _range);
			allDetails.Add("Decsription: Blah Blah Balh");
			return allDetails;
		}
		public override void Render()
		{
			SwinGame.FillRectangle(Color.CadetBlue, X, Y, Width, Height);
		}
		public override void Update()
		{

		}
	}
}
