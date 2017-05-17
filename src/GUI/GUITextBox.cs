using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class GUITextBox : GUIPart
	{
		float _x;
		float _y;
		float _height;
		float _width;
		GameObject _gameObj;
		const float _spacingperLine = 25;
		public GUITextBox(GameObject gameObj, float x, float y, float height, float width)
		{
			_x = x;
			_y = y;
			_height = height;
			_width = width;
			_gameObj = gameObj;
		}
		public override void Render()
		{
			SwinGame.FillRectangle(Color.White,_x,_y,_width,_height);
			float lineSpacing = 4;
			foreach (string line in _gameObj.AllDetails())
			{
				SwinGame.DrawText(line, Color.Black, _x + 4, _y + lineSpacing);
				lineSpacing += _spacingperLine;
			}
		}
	}
}
