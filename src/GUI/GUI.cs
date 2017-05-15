using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class GUI
	{
		GameGrid _gamegrid;
		Color _color;
		float _x;
		float _y;
		float _height;
		float _width;

		public GUI(GameGrid gamegrid)
		{
			_gamegrid = gamegrid;
			_color = Color.Cyan;
			_x = _gamegrid.Width;
			_y = 0;
			_width = SwinGame.ScreenWidth() - _gamegrid.Width;
			_height = _gamegrid.Height;
		}
		public void HandleInput()
		{

		}
		public void Render()
		{
			// Backgorund
			//SwinGame.FillRectangle(_color, _gamegrid.Width, 0, SwinGame.ScreenWidth() - _gamegrid.Width, _gamegrid.Height);
			SwinGame.FillRectangle(_color, _x, _y, _width, _height);
			// get selected rover 
			Rover seclectedR = _gamegrid.SelectedRover;
			SwinGame.DrawText(seclectedR.Name, Color.Black, "name", 20, _x +4, _y+4);
			//
		}
	}
}
