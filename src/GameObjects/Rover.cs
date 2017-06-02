using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// A rover is a class with a list of batteries List of device and  list of specimens
	/// It can attach and detach devices, Collect specimens, Move, Charge Batteries, Drill and Scan
	/// </summary>
	public class Rover : GameObject, IIsOwener
	{
		private List<Battery> _batteries;
		private List<Device> _devices;
		private List<Specimen> _specimens;
		private GameGrid _gamegrid;
		public Rover(string name, float width, float height, GameGrid gamegrind) : base(name, width, height)
		{
			_batteries = new List<Battery>();
			_devices = new List<Device>();
			_specimens = new List<Specimen>();
			_gamegrid = gamegrind;
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
		/// <summary>
		///  Every frame this function will run. 
		///  it will do base movement, check boundaries and charge batteries 
		/// </summary>
		public override void Update()
		{
			base.Update();
			_gamegrid.Checkbonders(this);
			ChargeBatteries();
		}
		/// <summary>
		/// This will operate the first camera in the List of Devices 
		/// </summary>
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
		/// <summary>
		/// This will find the battery with the most current power inside the battery list
		/// </summary>
		/// <returns></returns>
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
		/// <summary>
		/// This will  add a battery or a device to the battery list or device list
		/// and if it is a Device change the owner to this Rover
		/// </summary>
		/// <param name="attachItem"></param>
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
		/// <summary>
		/// This will  remove a device or battery and change the owner to the Rover’s game grid
		/// and if it's a battery, all devices that have that as there connected battery, there connected battery will be set to null 
		/// </summary>
		/// <param name="attachItem"></param>
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
				_gamegrid.Level.ClearRenderList();
			}
			else
			{
				_devices.RemoveAll(x => x == attachItem);
				(attachItem as Device).Owner = _gamegrid;
			}
		}
		/// <summary>
		/// Add a specimen to the specimen list
		/// </summary>
		/// <param name="newSpecimen"></param>
		public void Collect(Specimen newSpecimen)
		{
			_specimens.Add(newSpecimen);
			Console.WriteLine(Name + " has collected " + newSpecimen.Name + " weighing  " + newSpecimen.Weight);
		}

		/// <summary>
		/// For all the motors in the device list it changes of the direction of the motor And then operates it
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
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
		/// <summary>
		/// Makes all the solar panels in the device lists operate 
		/// </summary>
		/// <returns></returns>
		public bool ChargeBatteries()
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
		/// <summary>
		/// Makes all the drills in device list operate
		/// </summary>
		/// <returns></returns>
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
		/// <summary>
		/// Makes all the Radars in device list operate
		/// </summary>
		/// <returns></returns>
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
