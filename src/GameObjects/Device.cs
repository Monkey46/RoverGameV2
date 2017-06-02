using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
	/// <summary>
	/// An abstract class of devices that can be connected to a Rover 
	/// </summary>
	public abstract class Device : GameObject, IAttachable
	{
		/// <summary>
		/// The battery of which the device is connected too 
		/// </summary>
		private Battery _connectedbattery;

		private GameGrid _gamegrind;

		/// <summary>
		/// The Owner of the Device
		/// </summary>
		private IIsOwener _owner;
		public Device(string name, float width, float height, GameGrid gamegrind) : base(name, width, height)
		{
			_gamegrind = gamegrind;
			_owner = gamegrind;
		}
		public IIsOwener Owner
		{
			get { return _owner; }
			set { _owner = value; }
		}
		public Battery ConnectedBattery
		{
			get { return _connectedbattery; }
			set { _connectedbattery = value; }
		}
		public string ConnectedBatteryName
		{
			get { return _connectedbattery?.Name ?? "none"; }
		}
		public GameGrid GameGrid
		{
			get { return _gamegrind; }
		}

		/// <summary>
		/// Checks if it has a battery connected to it
		/// </summary>
		/// <returns></returns>
		public bool CheckBattery()
		{
			if (ConnectedBattery == null)
			{
				return false;
			}
			return true;
		}
		/// <summary>
		/// Check if the device can operate with the amount of power change 
		/// </summary>
		/// <param name="batChange"></param>
		/// <returns></returns>
		public bool CheckCanOperate(int batChange)
		{
			if (!CheckBattery())
			{
				return false;
			}
			if (!ConnectedBattery.ChangePower(batChange))
			{
				return false;
			}
			return true;
		}
		/// <summary>
		/// Each device will have to operate operate and 
		/// all it’s functionality of what the device can do
		/// </summary>
		/// <returns></returns>
		public abstract bool Operate();
	}
}
