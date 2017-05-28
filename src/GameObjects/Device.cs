using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
	public abstract class Device : GameObject, IAttachable
	{
		private Battery _connectedbattery;
		private GameGrid _gamegrind;
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

		public bool CheckBattery()
		{
			if (ConnectedBattery == null)
			{
				return false;
			}
			return true;
		}
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
		public abstract bool Operate();
	}
}
