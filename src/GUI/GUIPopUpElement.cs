﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// Pop up element is a part of a pop up 
	/// and each different child of pop-up element will have its own unique action functionality
	/// </summary>
	public abstract class GUIPopUpElement : GUIPart
	{
		string _name;
		GameGrid _gamegrid;
		GameObject _gameObj;
		public GUIPopUpElement(string name, float x, float y, float width, GameObject gameObj, GameGrid gamegrid)
		{
			_name = name;
			XPadding = 2;
			YPadding = 2;
			X = x;
			Y = y;
			Width = width;
			Height = 15;
			_gamegrid = gamegrid;
			_gameObj = gameObj;
		}
		public GameGrid GameGrid
		{
			get { return _gamegrid; }
		}
		public GameObject GameObj
		{
			get { return _gameObj; }
		}

		public override void Render()
		{
			SwinGame.FillRectangle(Color.AliceBlue,X+XPadding, Y+YPadding,Width-XPadding, Height);
			SwinGame.DrawText(_name, Color.Black, X + XPadding*2, Y + YPadding*2);
		}
		public abstract GUIPopUp Action();
	}
}
