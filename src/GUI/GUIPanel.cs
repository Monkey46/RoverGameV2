using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class GUIPanel
	{
		IHasDetails _gameObject;
		float _width;
		float _height;
		float _x;
		float _y;
		Color _panelColor;
		public GUIPanel(IHasDetails panelGO, Color panelColor, float x, float y, float height, float width)
		{
			_gameObject = panelGO;
			_height = height;
			_width = width;
			_x = x;
			_y = y;
			_panelColor = panelColor;
		}
		public GameObject GameObject
		{
			get { return _gameObject as GameObject; }
		}
		public bool IsAt(Point2D _point)
		{
			return SwinGame.PointInRect(_point, _x, _y, _width, _height);
		}
		public void Render()
		{
			SwinGame.FillRectangle(_panelColor, _x, _y, _width, _height);
			SwinGame.DrawText((_gameObject as GameObject).Name, Color.Black, _x + 4, _y + 4);
			SwinGame.DrawText(_gameObject.Details(), Color.Black, _x + 10, _y + 14);
		}
	}
}
