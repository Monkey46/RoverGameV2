using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// Motor is a device that moves The Rover
	/// </summary>
	public class Motor : Device
	{
		/// <summary>
		/// The direction in which the motor is going to move it's owner
		/// </summary>
		private Direction _direction;

		/// <summary>
		/// The amount the motor changes the owners velocity
		/// </summary>
		private float _maxSpeed;

		public Motor(string name, float maxspeed, GameGrid gamegrind) : this(name, 8, 5, maxspeed, gamegrind)
		{
		}
		public Motor(string name, float width, float height, float maxspeed, GameGrid gamegrind) : base(name, width, height, gamegrind)
		{
			_direction = Direction.none;
			_maxSpeed = maxspeed;
		}
		public Direction Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}
		/// <summary>
		/// Depending on the direction of the motor, It's owners velocity will get changed respectively 
		/// </summary>
		/// <returns></returns>
		public override bool Operate()
		{
			if (!CheckCanOperate(-1))
			{
				return false;
			}

			switch (_direction)
			{
				case Direction.up:
					(Owner as Rover).YVelocity += -_maxSpeed;
					break;
				case Direction.right:
					(Owner as Rover).XVelocity += _maxSpeed;
					break;
				case Direction.down:
					(Owner as Rover).YVelocity += _maxSpeed;
					break;
				case Direction.left:
					(Owner as Rover).XVelocity += -_maxSpeed;
					break;
			}
			return true;
		}
		public override string Details()
		{
			return "Motor Speed: " + _maxSpeed.ToString();
		}
		public override List<string> AllDetails()
		{
			List<string> allDetails = new List<string>();
			allDetails.Add(Name);
			allDetails.Add("Conected to " + ConnectedBatteryName);
			allDetails.Add("Speed: " + _maxSpeed);
			allDetails.Add("Decsription: Blah Blah Balh");
			return allDetails;
		}
		public override void Render()
		{
			SwinGame.FillRectangle(Color.DeepPink, X, Y, Width, Height);
		}
	}
}
