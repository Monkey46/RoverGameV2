using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// The graphical interface which the user will interact with 
	///	It can drop and pick up Game Objects
	/// Get more details of Game Objects
	/// </summary>
	public class GUI
	{
		private GameGrid _gamegrid;
		private Color _backgroundColor;
		private float _x;
		private float _y;
		private float _height;
		private float _width;

		/// <summary>
		/// List of all the GUI parts
		/// </summary>
		private List<GUIPart> _partList;
		/// <summary>
		///  List of all the GUI pop-up parts
		/// </summary>
		private List<GUIPopUp> _popUps;

		/// <summary>
		/// The padding between the edge of the background of the GUI and the edge of the panel
		/// </summary>
		private float _xPadding = 4;
		/// <summary>
		/// The padding between the edge of the background of the GUI and the edge of the panel
		/// </summary>
		private float _yPadding = 4;

		/// <summary>
		/// 
		/// </summary>
		private float _panelSpacing = 35;
		private float _panelHeight = 30;
		private Rover _seclectedRover;

		public GUI(GameGrid gamegrid)
		{
			_gamegrid = gamegrid;
			_backgroundColor = Color.Cyan;
			_x = _gamegrid.Width;
			_y = 0;
			_width = SwinGame.ScreenWidth() - _gamegrid.Width;
			_height = _gamegrid.Height;
			_partList = new List<GUIPart>();
			_popUps = new List<GUIPopUp>();
		}
		/// <summary>
		/// This handles the user input 
		/// this will create pop-ups and execute actions of the pop-up elements
		/// this will also change the selected object
		/// </summary>
		public void HandleInput()
		{
			if (SwinGame.MouseClicked(MouseButton.RightButton))
			{
				foreach (GUIPanel iGUIPanel in _partList.FindAll(x => x is GUIPanel))
				{
					if (iGUIPanel.IsAt(SwinGame.MousePosition()))
					{
						List<GUIPopUpElement> popupElements = MakePopUpElements(iGUIPanel);
						_popUps.Clear();
						_popUps.Add(new GUIPopUp(SwinGame.MousePosition(), iGUIPanel.GameObject, _gamegrid, popupElements));
					}
				}
			}
			if (SwinGame.MouseClicked(MouseButton.LeftButton))
			{
				if (_popUps.Count != 0)
				{
					PopUpChecker();
				}
				else
				{
					SelectPanel();
				}
			}
		}
		/// <summary>
		/// This will run every frame.
		/// It will clear the part list,
		/// and create panels of the selected Rover,
		/// and making information box of the selected object
		/// </summary>
		public void Update()
		{
			_seclectedRover = _gamegrid.SelectedRover;
			_partList.Clear();
			float spacing = 50 + _yPadding;
			foreach (Device dev in _seclectedRover.Devices)
			{
				Color panelColor;
				// If there's no connected battery the panel be red and if there is the panel be green 
				if (dev.ConnectedBattery == null)
				{ panelColor = Color.DarkRed; }
				else { panelColor = Color.ForestGreen; }
				GUIPanel s = new GUIPanel(dev, panelColor, _x + _xPadding, _y + spacing, _panelHeight, _width - _xPadding);
				spacing += _panelSpacing;
				_partList.Add(s);
			}
			foreach (Battery bat in _seclectedRover.Batteries)
			{
				GUIPanel s = new GUIPanel(bat, Color.BlueViolet, _x + _xPadding, _y + spacing, _panelHeight, _width - _xPadding);
				spacing += _panelSpacing;
				_partList.Add(s);
			}
			foreach (Specimen spec in _seclectedRover.Specimens)
			{
				GUIPanel s = new GUIPanel(spec, Color.WhiteSmoke, _x + _xPadding, _y + spacing, _panelHeight, _width - _xPadding);
				spacing += _panelSpacing;
				_partList.Add(s);
			}
			GUITextBox InfoBox = new GUITextBox(_gamegrid.Level.SelectedGameObject, _x + _xPadding, _y + spacing, 200, _width - _xPadding);
			_partList.Add(InfoBox);
		}
		/// <summary>
		/// will render the background of the GUI
		/// then render GUI parts
		/// then render the GUI pop-ups
		/// </summary>
		public void Render()
		{
			// Backgorund
			SwinGame.FillRectangle(_backgroundColor, _x, _y, _width, _height);
			SwinGame.DrawText(_seclectedRover.Name, Color.Red, _x + _xPadding, _y + _xPadding); // @Task Make this bigger some how?
			foreach (GUIPart iGUIPart in _partList)
			{
				iGUIPart.Render();
			}
			foreach (GUIPopUp popUp in _popUps)
			{
				popUp.Render();
			}
		}
		/// <summary>
		///  This will make a list of  GUI pop-up elements depending on the type of game object the GUI panel is
		/// </summary>
		/// <param name="iGUIPanel"></param>
		/// <returns></returns>
		private List<GUIPopUpElement> MakePopUpElements(GUIPanel iGUIPanel)
		{
			List<GUIPopUpElement> popupElements = new List<GUIPopUpElement>();
			GUIElementDrop drop = new GUIElementDrop(SwinGame.MousePosition().X, SwinGame.MousePosition().Y, 96, iGUIPanel.GameObject, _gamegrid);
			popupElements.Add(drop);
			if (iGUIPanel.GameObject is Device)
			{
				GUIElementConnect connect = new GUIElementConnect(SwinGame.MousePosition().X, SwinGame.MousePosition().Y + 20, 96, iGUIPanel.GameObject, _gamegrid);
				popupElements.Add(connect);
				if (iGUIPanel.GameObject is Drill)
				{
					GUIElementRepair repair = new GUIElementRepair(SwinGame.MousePosition().X, SwinGame.MousePosition().Y + 40, 96, iGUIPanel.GameObject, _gamegrid);
					popupElements.Add(repair);
				}
			}
			return popupElements;
		}
		/// <summary>
		/// Where the mouse position is it'll make that panel the selected object 
		/// </summary>
		private void SelectPanel()
		{
			foreach (GUIPanel iGUIPanel in _partList.FindAll(x => x is GUIPanel))
			{
				if (iGUIPanel.IsAt(SwinGame.MousePosition()))
				{
					_gamegrid.Level.SelectedGameObject = iGUIPanel.GameObject;
				}
			}
		}
		/// <summary>
		/// This will check if the mouse is over a pop up if not it will clear the pop-up
		/// else it will execute the action of the pop-up element
		/// </summary>
		private void PopUpChecker()
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
	}
}
