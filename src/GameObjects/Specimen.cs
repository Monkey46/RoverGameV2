using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class Specimen : GameObject
	{
		private float _size;
		private float _change;
		private int _count;
		private int _countMax;
		GameGrid _gamegrid;
		public Specimen(string name, float width, float height, float size, Color color, GameGrid gamegrid) : base(name, width, height)
		{
			_size = size;
			_change = 1;
			_count = 0;
			_countMax = 0;
			Color = color;
			_gamegrid = gamegrid;
		}
		public float Size
		{
			get { return _size; }
		}
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
			return "Size: " + _size;
		}
		public override List<string> AllDetails()
		{
			List<string> allDetails = new List<string>();
			allDetails.Add(Name);
			allDetails.Add("Size: " + Size);
			allDetails.Add("Decsription: Blah Blah Balh");
			return allDetails;
		}
		public override void Render()
		{
			SwinGame.FillRectangle(Color, X, Y, Width, Height);
		}

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
		private void Alternate()
		{
			float max = Width + Height;
			float min = 2;
			Alternate(max, min);
		}
		private void RandomMovment()
		{
			Random rand = _gamegrid.Level.Randomer;
			XVelocity = rand.Next(-1, 2);
			YVelocity = rand.Next(-1, 2);
		}
	}
}
