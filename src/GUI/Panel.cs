using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2.src
{
	public class Panel
	{
		GameObject _gameObject;
		float _width;
		float _hieght;
		float _x;
		float _y;
		Button dropButton;

		public Panel(GameObject panelGO, float height, float width, float x, float y)
		{
			_gameObject = panelGO;
			_hieght = height;
			_width = width;
			_x = x;
			_y = y;
		}
		public void Render()
		{
			SwinGame.FillRectangle(Color.Cyan, _x, _y, _width, _hieght);
			SwinGame.DrawText(_gameObject.Name, Color.Black, _x + 1, _y + 1);
			// @Task Insert Button class
			SwinGame.FillRectangle(Color.AliceBlue, _x, _y + 2, _width, _hieght - 4);



		}

	}
}
