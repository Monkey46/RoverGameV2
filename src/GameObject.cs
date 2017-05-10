using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace RoverGameV2
{
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
        public float X2
        {
            get { return _x + _width; }
        }
        public float Y2
        {
            get { return _y + _height; }
        }
        public float Width
        {
            get {return _width; }
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
        public float PreXVelocity
        {
            get { return _preXVelocity; }
            set { _preXVelocity = value; }
        }
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
            SwinGame.DrawRectangle(Color.Black,X-1,Y-1,Width+2,Height+2);
        }
        public abstract void Update();
        public abstract void Render();
        public bool IsAt(Point2D _point)
        {
            return SwinGame.PointInRect(_point, X, Y, Width, Height);
        }
        public void HasBeenScaned()
        {
            SwinGame.DrawCircle(Color.LightGreen, CenterX, CenterY, 3);
        }
        public void HasCollided(GameObject coilliedWith)
        {
            if (coilliedWith is Rover)
            {
                // needs Lots of Resreach and fixing
                _xVelocity = -_preXVelocity;
                _yVelocity = -_preYVelocity;
            }
        }
    }
}
