using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// The Drill will drill in small area collecting any specimens it finds and adding it to it's owner 
	/// </summary>
	public class Drill : Device
	{
		/// <summary>
		/// The amount of wear in percentage of the drill 
		/// </summary>
		private int _worn;

		private float _drillsize;

		public Drill(string name, float drillsize, GameGrid gamegrind) : this(name, 6, 5, drillsize, gamegrind)
		{
		}
		public Drill(string name, float width, float height, float drillsize, GameGrid gamegrind) : base(name, width, height, gamegrind)
		{
			_worn = 0;
			_drillsize = drillsize;
		}

		public int Worn
		{
			get { return _worn; }
			set { _worn = value; }
		}
		/// <summary>
		/// Asks the Gamegrid to detect specimens in its area then get owner to collect them
		/// </summary>
		/// <returns></returns>
		public override bool Operate()
		{

			if (!CheckCanOperate(-2))
			{
				return false;
			}
			// If worn is 100% it is 20% chance to fail Operating 
			if (_worn >= 100)
			{
				int chance = GameGrid.Level.Randomer.Next(0, 100);
				if (chance <= 20)
				{
					Console.WriteLine(Name + " failed to drill");
					return false;
				}
			}
			// Make drill area 
			Circle drillArea = new Circle();
			drillArea.Center = (Owner as Rover).Center;
			drillArea.Radius = _drillsize;

			List<Specimen> drilledGOs = GameGrid.GetDrilledSpecimen(drillArea);

			if (drilledGOs.Count == 0)
			{
				_worn += 10;
				WornAt100();
				return false;
			}

			// Makes the owner collect the item and remove them from the level list 
			foreach (GameObject singleDrilledGO in drilledGOs)
			{
				(Owner as Rover).Collect(singleDrilledGO as Specimen);
				GameGrid.Level.LevelGameObjects.Remove(singleDrilledGO);
			}
			_worn += 5;
			WornAt100();
			return true;
		}
		public override string Details()
		{
			return "Drill Size: " + _drillsize.ToString() + " Worn: " + _worn.ToString() + "%";
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
		/// <summary>
		/// Make sure worn doesn't go past 100%
		/// </summary>
		private void WornAt100()
		{
			if (_worn > 100)
			{
				_worn = 100;
			}
		}
	}
}

