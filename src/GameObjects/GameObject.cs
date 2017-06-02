using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace RoverGameV2
{
	/// <summary>
	/// GameObjects all objects that are within the game grid and are part of the main gameplay 
	/// </summary>
	public abstract class GameObject
	{
		private string _name;
		private float _x;
		private float _y;
		private float _width;
		private float _height;
		private float _xVelocity;
		private float _yVelocity;
		private float _preXVelocity;
		private float _preYVelocity;
		public GameObject(string name, float width, float height)
		{
			_name = name;
			_width = width;
			_height = height;

		}
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
		public float X
		{
			get { return _x; }
			set { _x = value; }
		}
		public float Y
		{
			get { return _y; }
			set { _y = value; }
		}
		/// <summary>
		/// the far right end point of the object
		/// </summary>
		public float X2
		{
			get { return _x + _width; }
			set { _x = value - _width; }
		}
		/// <summary>
		/// The far down and point of the object 
		/// </summary>
		public float Y2
		{
			get { return _y + _height; }
			set { _y = value - _height; }
		}
		public float Width
		{
			get { return _width; }
			set { _width = value; }
		}
		public float Height
		{
			get { return _height; }
			set { _height = value; }
		}
		public float CenterX
		{
			get { return X + (Width / 2); }
		}
		public float CenterY
		{
			get { return Y + (Height / 2); }
		}
		public Point2D Center
		{
			get
			{
				Point2D center = new Point2D();
				center.X = CenterX;
				center.Y = CenterY;
				return center;
			}
		}
		public float XVelocity
		{
			get { return _xVelocity; }
			set { _xVelocity = value; }
		}
		public float YVelocity
		{
			get { return _yVelocity; }
			set { _yVelocity = value; }
		}
		/// <summary>
		/// the Object’s previous X velocity 
		/// </summary>
		public float PreXVelocity
		{
			get { return _preXVelocity; }
			set { _preXVelocity = value; }
		}
		/// <summary>
		/// the Object’s previous Y velocity 
		/// </summary>
		public float PreYVelocity
		{
			get { return _preYVelocity; }
			set { _preYVelocity = value; }
		}
		public Rectangle HitBox
		{
			get
			{
				Rectangle hitbox = new Rectangle();
				hitbox.X = X - 1;
				hitbox.Y = Y - 1;
				hitbox.Width = Width + 2;
				hitbox.Height = Height + 2;
				return hitbox;
			}
		}
		public void RederOutline()
		{
			SwinGame.DrawRectangle(Color.Black, X - 1, Y - 1, Width + 2, Height + 2);
		}
		/// <summary>
		/// The method that tells us how to render this object
		/// </summary>
		public abstract void Render();
		/// <summary>
		///  a method that gives us a list of strings of all the needed information about the subject
		///  to be used in the GUI
		/// </summary>
		/// <returns></returns>
		public abstract List<string> AllDetails();
		/// <summary>
		/// a method that gives us short description of the object
		/// to b used int the GUI
		/// </summary>
		/// <returns></returns>
		public abstract string Details();

		/// <summary>
		/// Find out if the object is at this point
		/// </summary>
		/// <param name="_point"></param>
		/// <returns></returns>
		public bool IsAt(Point2D _point)
		{
			return SwinGame.PointInRect(_point, X, Y, Width, Height);
		}

		/// <summary>
		/// If an object has collided with a rover it will go back to its previous velocity 
		/// </summary>
		/// <param name="coilliedWith"></param>
		public void HasCollided(GameObject coilliedWith)
		{
			// @Task Make to abstruct Method
			if (coilliedWith is Rover)
			{
				// @Task needs Lots of Resreach and fixing
				_xVelocity = -(2 * _preXVelocity);
				_yVelocity = -(2 * _preYVelocity);
			}

		}
		/// <summary>
		/// Each object will have to update itself per frame and each object Will already have update movement as it's base
		/// </summary>
		public virtual void Update()
		{
			UpdateMovement();
		}
		/// <summary>
		/// Changes the object's coordinates  associated with X and Y velocities and then resets the X and Y velocities
		/// </summary>
		private void UpdateMovement()
		{
			X += XVelocity;
			Y += YVelocity;
			PreXVelocity = XVelocity;
			PreYVelocity = YVelocity;
			XVelocity = 0;
			YVelocity = 0;
		}
	}
}
