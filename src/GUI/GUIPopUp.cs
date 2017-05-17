using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace RoverGameV2
{
	public class GUIPopUp : GUIPart
	{
		Color _color = Color.DeepSkyBlue;
		GameObject _gameObj;
		GameGrid _gamegrid;
		List<GUIPopUpElement> _elements;

		public GUIPopUp(Point2D mousePos, GameObject gameObj, GameGrid gamegrid)
		{
			X = mousePos.X;
			Y = mousePos.Y;
			Width = 100;
			Height = 60;
			_gameObj = gameObj;
			_elements = new List<GUIPopUpElement>();
			_gamegrid = gamegrid;
		}
		public List<GUIPopUpElement> Elements
		{
			get { return _elements; }
		}
		public override void Render()
		{
			_elements.Clear();
			SwinGame.FillRectangle(_color, X, Y, Width, Height);
			GUIElementDrop drop = new GUIElementDrop(X, Y, Width, _gameObj, _gamegrid);
			_elements.Add(drop);
			//GUIPopUpElement Connect = new GUIPopUpElement();
			drop.Render();
		}
		public bool IsAt(Point2D _point)
		{
			return SwinGame.PointInRect(_point, X, Y, Width, Height);
		}
	}
}
