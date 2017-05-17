using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class Rover : GameObject, IIsOwener
	{
		private List<Battery> _batteries;
		private List<Device> _devices;
		private List<Specimen> _specimens;
		private GameGrid _gamegrind;
		public Rover(string name, float width, float height, GameGrid gamegrind) : base(name, width, height)
		{
			_batteries = new List<Battery>();
			_devices = new List<Device>();
			_specimens = new List<Specimen>();
			_gamegrind = gamegrind;
		}

		public List<Battery> Batteries
		{
			get { return _batteries; }
		}
		public List<Device> Devices
		{
			get { return _devices; }
		}
		public List<Specimen> Specimens
		{
			get { return _specimens; }
		}
		public override void Render()
		{
			SwinGame.FillRectangle(Color.Red, X, Y, Width, Height);
			SwinGame.DrawText("R", Color.Black, X + 1, Y + 1);
		}

		public override void Update()
		{
			X += XVelocity;
			Y += YVelocity;
			PreXVelocity = XVelocity;
			PreYVelocity = YVelocity;
			XVelocity = 0;
			YVelocity = 0;
		}
		public void UpdateRenderList()
		{
			Camera camera = _devices.Find(x => x is Camera) as Camera;
			if (camera != null)
			{
				camera.Operate();
			}
		}
		public override string Details()
		{
			return "";
		}
		public override List<string> AllDetails()
		{
			List<string> allDetails = new List<string>();
			allDetails.Add(Name);
			allDetails.Add("Has " + _devices.Count + " Devices");
			allDetails.Add("Has " + _batteries.Count + " Batteries Connected and  Current Total power is " + TotalPower);
			allDetails.Add("Has " + _specimens.Count + " Specimens");
			allDetails.Add("Decsription: Blah Blah Balh");
			return allDetails;
		}
		private int TotalPower
		{
			get
			{
				int total = 0;
				foreach (Battery bat in _batteries)
				{ total += bat.Power; }
				return total;
			}
		}
		private Battery HighestBattery()
		{
			if (_batteries.Count == 0)
			{
				return null;
			}
			Battery highestBat = _batteries.First();
			foreach (Battery bat in _batteries)
			{
				if (highestBat.Power < bat.Power)
				{
					highestBat = bat;
				}
			}
			return highestBat;
		}
		public void Attach(IAttachable attachItem)
		{
			if (attachItem is Battery)
			{
				_batteries.Add(attachItem as Battery);
			}
			else
			{
				(attachItem as Device).Owner = this;
				_devices.Add(attachItem as Device);

				(attachItem as Device).ConnectedBattery = HighestBattery();
			}
		}
		public void Detach(IAttachable attachItem)
		{
			if (attachItem is Battery)
			{
				_batteries.RemoveAll(x => x == attachItem);
				foreach (Device dev in _devices)
				{
					if (dev.ConnectedBattery == attachItem)
					{
						dev.ConnectedBattery = null;
					}
				}
				_gamegrind.Level.RenderList.Clear();
			}
			else
			{
				_devices.RemoveAll(x => x == attachItem);
				(attachItem as Device).Owner = _gamegrind;
			}
		}
		public void Collect(Specimen newSpecimen)
		{
			_specimens.Add(newSpecimen);
			Console.WriteLine(Name + " has collected " + newSpecimen.Name + " weighing  " + newSpecimen.Size);
		}

		public bool Move(Direction direction)
		{
			if (_devices.Exists(x => x is Motor))
			{
				foreach (Motor motor in _devices.FindAll(m => m is Motor))
				{
					motor.Direction = direction;
					motor.Operate();
				}
				return true;
			}
			return false;
		}
		public bool Charge()
		{
			if (_devices.Exists(x => x is SolarPanel))
			{
				foreach (SolarPanel Sp in _devices.FindAll(s => s is SolarPanel))
				{
					Sp.Operate();
				}
				return true;
			}
			return false;
		}
		public bool Drill()
		{
			if (_devices.Exists(x => x is Drill))
			{
				foreach (Drill drill in _devices.FindAll(s => s is Drill))
				{
					if (drill.Operate())
					{
						return true;
					}
				}
			}
			return false;
		}
		public bool Scan()
		{
			if (_devices.Exists(x => x is Radar))
			{
				foreach (Radar radar in _devices.FindAll(s => s is Radar))
				{
					if (radar.Operate())
					{
						return true;
					}
				}
			}
			return false;
		}


	}
}
