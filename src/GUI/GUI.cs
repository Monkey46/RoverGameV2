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
		HashSet<GUIPanel> _panelList;

		public GUI(GameGrid gamegrid)
		{
			_gamegrid = gamegrid;
			_color = Color.Cyan;
			_x = _gamegrid.Width;
			_y = 0;
			_width = SwinGame.ScreenWidth() - _gamegrid.Width;
			_height = _gamegrid.Height;
			_panelList = new HashSet<GUIPanel>();
		}
		public void HandleInput()
		{
			if (SwinGame.MouseClicked(MouseButton.RightButton))
			{
				GUIPanel removePanel = null;
				foreach (GUIPanel iGUIPanel in _panelList)
				{
					if (iGUIPanel.IsAt(SwinGame.MousePosition()))
					{
						_gamegrid.Drop(iGUIPanel.GameObject);
						removePanel = iGUIPanel;
					}
				}
				_panelList.Remove(removePanel);

			}
			if (SwinGame.MouseClicked(MouseButton.LeftButton))
			{
				foreach (GUIPanel iGUIPanel in _panelList)
				{
					if (iGUIPanel.IsAt(SwinGame.MousePosition()))
					{
						_gamegrid.Level.SelectedGameObject = iGUIPanel.GameObject;
					}
				}
			}
		}
		public void Render()
		{
			_panelList.Clear();
			// Backgorund
			//SwinGame.FillRectangle(_color, _gamegrid.Width, 0, SwinGame.ScreenWidth() - _gamegrid.Width, _gamegrid.Height);
			SwinGame.FillRectangle(_color, _x, _y, _width, _height);
			// get selected rover 
			Rover seclectedR = _gamegrid.SelectedRover;
			SwinGame.DrawText(seclectedR.Name, Color.Red, _x + 4, _y + 4);
			float panelSpacing = 54;
			foreach (Device dev in seclectedR.Devices)
			{
				GUIPanel s = new GUIPanel(dev, Color.ForestGreen, _x + 4, _y + panelSpacing, 30, _width - 4);
				s.Render();
				panelSpacing += 40;
				_panelList.Add(s);
			}
			foreach (Battery bat in seclectedR.Batteries)
			{
				GUIPanel s = new GUIPanel(bat, Color.BlueViolet, _x + 4, _y + panelSpacing, 30, _width - 4);
				s.Render();
				panelSpacing += 40;
				_panelList.Add(s);
			}
			foreach (Specimen spec in seclectedR.Specimens)
			{
				GUIPanel s = new GUIPanel(spec, Color.WhiteSmoke, _x + 4, _y + panelSpacing, 30, _width - 4);
				s.Render();
				panelSpacing += 40;
				_panelList.Add(s);
			}
			// render Info box
			GUITextBox InfoBox = new GUITextBox(_gamegrid.Level.SelectedGameObject, _x + 4, _y + panelSpacing, 200, _width - 4);
			InfoBox.Render();
		}
	}
}
