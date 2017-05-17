using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public abstract class GUIPopUpElement : GUIPart
	{
		string _name;
		GameGrid _gamegrid;
		GameObject _gameObj;
		public GUIPopUpElement(string name, float x, float y, float width, GameObject gameObj, GameGrid gamegrid)
		{
			_name = name;
			XPadding = 4;
			YPadding = 4;
			X = x;
			Y = y;
			Width = width;
			Height = 15;
			_gamegrid = gamegrid;
			_gameObj = gameObj;
		}
		public GameGrid GameGrid
		{
			get { return _gamegrid; }
		}
		public GameObject GameObj
		{
			get { return _gameObj; }
		}
		public override void Render()
		{
			SwinGame.FillRectangle(Color.AliceBlue,X+XPadding, Y+YPadding,Width-XPadding, Height);
			SwinGame.DrawText(_name, Color.Black, X + XPadding, Y + YPadding);
		}
		public bool IsAt(Point2D _point)
		{
			return SwinGame.PointInRect(_point, X, Y, Width, Height);
		}
		public abstract void Action();
	}
}
