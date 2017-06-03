using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// A panel is there a rectangular square that displays a single game object’s name and basic details
	/// </summary>
	public class GUIPanel : GUIPart
	{
		GameObject _gameObject;
		Color _panelColor;
		public GUIPanel(GameObject panelGO, Color panelColor, float x, float y, float height, float width)
		{
			_gameObject = panelGO;
			Height = height;
			Width = width;
			X = x;
			Y = y;
			_panelColor = panelColor;
		}
		public GameObject GameObject
		{
			get { return _gameObject as GameObject; }
		}

		/// <summary>
		/// Draws the background, then draws its Game Object's name, then draws Game Object's details 
		/// </summary>
		public override void Render()
		{
			SwinGame.FillRectangle(_panelColor, X, Y, Width, Height);
			SwinGame.DrawText((_gameObject as GameObject).Name, Color.Black, X + 4, Y + 4);
			SwinGame.DrawText(_gameObject.Details(), Color.Black, X + 10, Y + 14);
		}
	}
}
