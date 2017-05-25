using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class Drill : Device
	{
		private int _worn;
		private float _drillsize;
		public Drill(string name, float drillsize, GameGrid gamegrind) : base(name, gamegrind)
		{
			_worn = 0;
			_drillsize = drillsize;
		}
		public Drill(string name, float width, float height, float drillsize, GameGrid gamegrind) : base(name, width, height, gamegrind)
		{
			_worn = 0;
			_drillsize = drillsize;
		}

		public int Worn
		{
			get { return _worn; }
		}
		public override bool Operate()
		{
			if (_worn >= 100)
			{
				Random rand = new Random();
				int chance = rand.Next(0, 100);
				if (chance <= 20)
				{
					Console.WriteLine(Name + " failed to drill");
					return false;
				}
			}

			Circle drillArea = new Circle();
			drillArea.Center = (Owner as Rover).Center;
			drillArea.Radius = _drillsize;

			List<Specimen> drilledGOs = GameGrid.GetDrilledSpecimen(drillArea);
			if (drilledGOs.Count == 0)
			{
				_worn += 10;
				return false;
			}
			foreach (GameObject singleDrilledGO in drilledGOs)
			{
				(Owner as Rover).Collect(singleDrilledGO as Specimen);
				GameGrid.Level.LevelGameObjects.Remove(singleDrilledGO);
			}
			_worn += 5;
			return true;
		}
		public override string Details()
		{
			return "Drill Size: " + _drillsize.ToString() + " Worn: " + _worn.ToString();
		}
		public override List<string> AllDetails()
		{
			List<string> allDetails = new List<string>();
			allDetails.Add(Name);
			allDetails.Add("Conected to " + ConnectedBatteryName);
			allDetails.Add("Drill Size: " + _drillsize);
			allDetails.Add("Worn: " + Worn + "%");
			allDetails.Add("Decsription: Blah Blah Balh");
			return allDetails;
		}
		public override void Render()
		{
			SwinGame.FillRectangle(Color.DeepPink, X, Y, Width, Height);
		}

		public override void Update()
		{
		}
	}
}

