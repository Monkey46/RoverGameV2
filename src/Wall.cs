using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class Wall
	{
		public Wall(float x,float y, float x2, float y2)
		{
			X = x;
			Y = y;
			X2 = x2;
			Y2 = y2;
		}
		float X
		{ get; set; }
		float Y
		{ get; set; }
		float X2
		{ get; set; }
		float Y2
		{ get; set; }


	}
}
