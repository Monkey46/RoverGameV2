using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class Camera : Device
    {
        float _range;

        public Camera(string name, float width, float height, float range, GameGrid gamegrid) : base(name, width, height, gamegrid)
        {
            _range = range;
        }
        public override bool Operate()
        {
            if (!CheckBattery())
            {
                Console.WriteLine(Name + " has no Battery conected");
                return false;
            }
            if (!ConnectedBattery.ChangePower(0))
            {
                Console.WriteLine(ConnectedBattery.Name + " doesn't have enough charge to power " + Name);
                return false;
            }

            return true;

        }
        public override void Render()
        {
            
        }
        public override void Update()
        {

        }
    }
}
