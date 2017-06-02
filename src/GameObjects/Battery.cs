using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// This class is a battery which is used to power Devices in The Rover 
	/// </summary>
	public class Battery : GameObject, IAttachable

	{
		/// <summary>
		/// This is the current power level of the battery
		/// </summary>
		private int _powerlvl;
		/// <summary>
		/// This is the max power level the battery can be at
		/// </summary>
		private int _MaxLVl;

		public Battery(string name, float width, float height, int MaxPowerlvl) : base(name, width, height)
		{
			_MaxLVl = MaxPowerlvl;

			//  The batteries initial power starts at half it's MaxPower 
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
		/// <summary>
		/// This method would change the power level of the battery and
		/// if the change is too negative and a batteries less then 0 better returns false 
		/// </summary>
		/// <param name="powerchange"></param>
		/// <returns></returns>
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
	}
}
