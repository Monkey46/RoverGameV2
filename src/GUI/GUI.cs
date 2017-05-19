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
		List<GUIPart> _partList;
		List<GUIPopUp> _popUps;
		float _xPadding = 4;
		float _yPadding = 4;
		float _panelSpacing = 35;
		float _panelHeight = 30;
		Rover _seclectedR;

		public GUI(GameGrid gamegrid)
		{
			_gamegrid = gamegrid;
			_color = Color.Cyan;
			_x = _gamegrid.Width;
			_y = 0;
			_width = SwinGame.ScreenWidth() - _gamegrid.Width;
			_height = _gamegrid.Height;
			_partList = new List<GUIPart>();
			_popUps = new List<GUIPopUp>();
		}
		public void HandleInput()
		{
			if (SwinGame.MouseClicked(MouseButton.RightButton))
			{
				foreach (GUIPanel iGUIPanel in _partList.FindAll(x => x is GUIPanel))
				{
					if (iGUIPanel.IsAt(SwinGame.MousePosition()))
					{
						List<GUIPopUpElement> popupElements = new List<GUIPopUpElement>();
						GUIElementDrop drop = new GUIElementDrop(SwinGame.MousePosition().X, SwinGame.MousePosition().Y, 96, iGUIPanel.GameObject, _gamegrid);
						popupElements.Add(drop);
						if (iGUIPanel.GameObject is Device)
						{
							GUIElementConnect connect = new GUIElementConnect(SwinGame.MousePosition().X, SwinGame.MousePosition().Y + 20, 96, iGUIPanel.GameObject, _gamegrid);
							popupElements.Add(connect);
						}
						_popUps.Clear();
						_popUps.Add(new GUIPopUp(SwinGame.MousePosition(), iGUIPanel.GameObject, _gamegrid, popupElements));
					}
				}
			}
			if (SwinGame.MouseClicked(MouseButton.LeftButton))
			{
				if (_popUps.Count != 0)
				{
					bool clearpopUp = false;
					GUIPopUp temp = null;
					foreach (GUIPopUp popup in _popUps)
					{
						if (popup.IsAt(SwinGame.MousePosition()))
						{
							foreach (GUIPopUpElement ele in popup.Elements)
							{
								if (ele.IsAt(SwinGame.MousePosition()))
								{
									GUIPopUp newPopUp = ele.Action();
									if (newPopUp == null)
									{
										clearpopUp = true;
									}
									else
									{
										temp = newPopUp;
									}
								}
							}
						}
						else clearpopUp = true;
					}
					_popUps.Add(temp);
					if (clearpopUp == true)
					{
						_popUps.Clear();
					}	
				}
				else
				{
					foreach (GUIPanel iGUIPanel in _partList.FindAll(x => x is GUIPanel))
					{
						if (iGUIPanel.IsAt(SwinGame.MousePosition()))
						{
							_gamegrid.Level.SelectedGameObject = iGUIPanel.GameObject;
						}
					}
				}
			}
		}
		public void Update()
		{
			_seclectedR = _gamegrid.SelectedRover;
			_partList.Clear();
			float spacing = 50 + _yPadding;
			foreach (Device dev in _seclectedR.Devices)
			{
				Color panelColor;
				if (dev.ConnectedBattery == null)
				{ panelColor = Color.DarkRed; }
				else { panelColor = Color.ForestGreen; }
				GUIPanel s = new GUIPanel(dev, panelColor, _x + _xPadding, _y + spacing, _panelHeight, _width - 4);
				spacing += _panelSpacing;
				_partList.Add(s);
			}
			foreach (Battery bat in _seclectedR.Batteries)
			{
				GUIPanel s = new GUIPanel(bat, Color.BlueViolet, _x + _xPadding, _y + spacing, _panelHeight, _width - 4);
				spacing += _panelSpacing;
				_partList.Add(s);
			}
			foreach (Specimen spec in _seclectedR.Specimens)
			{
				GUIPanel s = new GUIPanel(spec, Color.WhiteSmoke, _x + _xPadding, _y + spacing, _panelHeight, _width - 4);
				spacing += _panelSpacing;
				_partList.Add(s);
			}
			GUITextBox InfoBox = new GUITextBox(_gamegrid.Level.SelectedGameObject, _x + _xPadding, _y + spacing, 200, _width - 4);
			_partList.Add(InfoBox);
		}
		public void Render()
		{
			// Backgorund
			SwinGame.FillRectangle(_color, _x, _y, _width, _height);
			SwinGame.DrawText(_seclectedR.Name, Color.Red, _x + _xPadding, _y + _xPadding); // @Task Make this bigger some how?
			foreach (GUIPart iGUIPart in _partList)
			{
				iGUIPart.Render();
			}
			foreach(GUIPopUp popUp in _popUps)
			{
				popUp.Render();
			}
		}
	}
}
