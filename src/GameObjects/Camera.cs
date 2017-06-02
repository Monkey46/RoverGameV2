using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// The camera class this will detect objects around it and tell the game to Render those objects 
	/// </summary>
	public class Camera : Device
	{
		private float _range;
		public Camera(string name, float width, float height, float range, GameGrid gamegrid) : base(name, width, height, gamegrid)
		{
			_range = range;
		}
		/// <summary>
		/// Gets a list of GameObjects from the GameGrid and then makes that the levels RenderList
		/// </summary>
		/// <returns></returns>
		public override bool Operate()
		{
			if (!CheckCanOperate(-1))
			{
				GameGrid.Level.RenderList = null;
				return false;
			}
			// Make the view area of the camera
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
	}
}
