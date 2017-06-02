using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// Specimen is a random moving object that the rover needs to  drill and collect
	/// </summary>
	public class Specimen : GameObject
	{
		/// <summary>
		/// How much the object weighs
		/// </summary>
		private float _weight;

		/// <summary>
		///  how fast the object alternates
		/// </summary>
		private float _change;

		/// <summary>
		///  alternate variable
		/// </summary>
		private int _count;
		/// <summary>
		///  alternate variable
		/// </summary>
		private int _countMax;

		GameGrid _gamegrid;
		public Specimen(string name, float width, float height, float size, Color color, GameGrid gamegrid) : base(name, width, height)
		{
			_weight = size;
			_change = 1;
			_count = 0;
			_countMax = 0;
			Color = color;
			_gamegrid = gamegrid;
		}
		public float Weight
		{
			get { return _weight; }
		}
		/// <summary>
		/// Set the Alternate speed from 1 to 10 
		/// </summary>
		public int AlternateSpeed
		{
			set
			{
				if (value <= 5)
				{
					_change = 1;
					_countMax = 5 - value;
				}
				if (value > 5)
				{
					_change = value - 5;
					_countMax = 0;
				}
			}
		}
		public Color Color
		{
			get;
			set;
		}

		public override string Details()
		{
			return "Weight: " + _weight;
		}
		public override List<string> AllDetails()
		{
			List<string> allDetails = new List<string>();
			allDetails.Add(Name);
			allDetails.Add("Weight: " + Weight);
			allDetails.Add("Decsription: Blah Blah Balh");
			return allDetails;
		}
		public override void Render()
		{
			SwinGame.FillRectangle(Color, X, Y, Width, Height);
		}
		/// <summary>
		/// Every frame this function will run. 
		///  it will do base movement, check boundaries and Alternate
		/// </summary>
		public override void Update()
		{
			base.Update();
			RandomMovment();
			_gamegrid.Checkbonders(this);
			if (_count == _countMax)
			{
				Alternate();
				_count = 0;
			}
			else _count++;
		}
		/// <summary>
		/// Changes the height and width
		///	If height is increasing then width is decreasing and vice a versa
		///	until it either hits it's maximum point or minimum point And then it reverses the other way 
		/// </summary>
		private void Alternate()
		{
			float max = Width + Height;
			float min = 2;
			Alternate(max, min);
		}
		private void Alternate(float max, float min)
		{
			if (Height >= max || Width >= max || Height <= min || Width <= min)
			{
				_change = -1 * _change;
			}
			X = X + _change;
			Width = Width - 2 * _change;
			Y = Y - _change;
			Height = Height + 2 * _change;
		}
		/// <summary>
		/// Give the specimen random X velocity and Y velocity
		/// </summary>
		private void RandomMovment()
		{
			Random rand = _gamegrid.Level.Randomer;
			XVelocity = rand.Next(-1, 2);
			YVelocity = rand.Next(-1, 2);
		}
	}
}
