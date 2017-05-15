using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class SolarPanel : Device
	{
		int _chargeRate;
		public SolarPanel(string name, int chargeRate, GameGrid gamegrind) : base(name, gamegrind)
		{
			_chargeRate = chargeRate;
		}
		public SolarPanel(string name, float width, float height, int chargeRate, GameGrid gamegrind) : base(name, width, height, gamegrind)
		{
			_chargeRate = chargeRate;
		}
		public override bool Operate()
		{
			if (!CheckBattery())
			{
				Console.WriteLine(Name + " has no Battery conected");
				return false;
			}
			ConnectedBattery.ChangePower(_chargeRate);
			Console.Write(Name + " has charged " + ConnectedBattery.Name + " by 1\n");
			return true;
		}
		public override string Details()
		{
			return "Charge Rate: " + _chargeRate.ToString();
		}
		public override void Render()
		{
			SwinGame.FillRectangle(Color.DeepPink, X, Y, Width, Height);
		}
		// @Paul empty?
		public override void Update()
		{
		}
	}
}
