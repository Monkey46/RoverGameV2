using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// An abstract class  that has all the common fields and functionality to be a GUI part
	/// </summary>
	public abstract class GUIPart
	{
		public float X
		{ get; set; }
		public float Y
		{ get; set; }
		public float Height
		{ get; set; }
		public float Width
		{ get; set; }
		public float XPadding
		{ get; set; }
		public float YPadding
		{ get; set; }

		/// <summary>
		/// Find out if the GUI Part is at this point
		/// </summary>
		/// <param name="_point"></param>
		/// <returns></returns>
		public bool IsAt(Point2D _point)
		{
			return SwinGame.PointInRect(_point, X, Y, Width, Height);
		}

		public abstract void Render();


	}
}
