using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class Battery : GameObject, IAttachable

	{
		private int _powerlvl;
		private int _MaxLVl;
		public Battery(string name, float width, float height, int MaxPowerlvl) : base(name, width, height)
		{
			_MaxLVl = MaxPowerlvl;
			_powerlvl = MaxPowerlvl / 2;
		}
		public int Power
		{
			get { return _powerlvl; }
		}
		public int MaxPowerLevel
		{
			get { return _MaxLVl; }
		}
		public bool ChangePower(int powerchange)
		{
			if (_powerlvl + powerchange < 0)
			{
				return false;
			}
			_powerlvl = _powerlvl + powerchange;
			if (_powerlvl > _MaxLVl)
			{
				_powerlvl = _MaxLVl;
			}
			return true;
		}

		public override string Details()
		{
			return "Power Level: " + _powerlvl.ToString() + "/" + _MaxLVl.ToString(); ;
		}
		public override List<string> AllDetails()
		{
			List<string> allDetails = new List<string>();
			allDetails.Add(Name);
			allDetails.Add("Current Power Level: " + Power + "/" + MaxPowerLevel);
			allDetails.Add("Decsription: Blah Blah Balh");
			return allDetails;
		}

		public override void Render()
		{
			SwinGame.FillRectangle(Color.Blue, X, Y, Width, Height);
		}
		public override void Update()
		{

		}
	}
}
