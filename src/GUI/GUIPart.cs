using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
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
		
		public abstract void Render();


	}
}
