using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class SolarPanel : Device
    {
        public SolarPanel(string name, float width, float height, GameGrid gamegrind) : base(name, width, height,gamegrind)
        {
        }
        public override bool Operate()
        {
            if (!CheckBattery())
            {
                Console.WriteLine(Name + " has no Battery conected");
                return false;
            }
            ConnectedBattery.ChangePower(1);
            Console.Write(Name + " has charged " + ConnectedBattery.Name + " by 1\n");
            return true;
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
