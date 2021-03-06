﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
	/// <summary>
	/// This GUI pop-up element will drop the panel’s game object 
	/// </summary>
	public class GUIElementDrop : GUIPopUpElement
	{
		public GUIElementDrop(float x, float y, float width, GameObject gameObj, GameGrid gamegrid) : base("Drop", x, y, width, gameObj, gamegrid)
		{

		}

		public override GUIPopUp Action()
		{
			GameGrid.Drop(GameObj);
			return null;
		}
	}
}
