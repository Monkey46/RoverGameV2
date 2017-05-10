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
        float _size;
        float _change;
        int _count;
        public Specimen(string name, float width, float height, float size) : base(name, width, height)
        {
            _size = size;
            _change = 1;
            _count = 0;
        }
        public float Size
        {
            get { return _size; }
        }
        public override void Render()
        {
            SwinGame.FillRectangle(Color.Green, X, Y, Width, Height);
        }

        public override void Update()
        {
            if (_count == 4)
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
            double max =  Width + Height ;
            double min = 2;

            X = X + _change;
            Width = Width - 2 * _change;
            Y = Y - _change;
            Height = Height + 2 * _change;
            if (Height >= max || Width >= max || Height <= min || Width <= min)
            {
                _change = -1 * _change;
            }
        }
    }
}
