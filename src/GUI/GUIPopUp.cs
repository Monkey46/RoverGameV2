using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace RoverGameV2
{
	/// <summary>
	/// A rectangular box with a list of GUI pop up elements
	/// </summary>
	public class GUIPopUp : GUIPart
	{
		Color _color = Color.DeepSkyBlue;
		GameObject _gameObj;
		GameGrid _gamegrid;
		List<GUIPopUpElement> _elements;

		public GUIPopUp(Point2D mousePos, GameObject gameObj, GameGrid gamegrid, List<GUIPopUpElement> elements)
		{
			X = mousePos.X;
			Y = mousePos.Y;
			Width = 100;
			Height = elements.Count *20;
			_gameObj = gameObj;
			_elements = elements;
			_gamegrid = gamegrid;
		}
		public List<GUIPopUpElement> Elements
		{
			get { return _elements; }
		}
		/// <summary>
		/// Will render the pop-up background then render all the popup elements in the elements list
		/// </summary>
		public override void Render()
		{
			SwinGame.FillRectangle(_color, X, Y, Width, Height);
			foreach (GUIPopUpElement element in _elements)
			{
				element.Render();
			}
		}
	}
}
