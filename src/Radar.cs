using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class Radar : Device
    {
        private Type _type;
        private int _range;
        public Radar(string name, Type type, int range, GameGrid gamegrind) : base (name, gamegrind)
        {
            _type = type;
            _range = range;
            // @Task Is there a way here to not duplicate code
        }
        public Radar(string name, float width, float height, Type type, int range, GameGrid gamegrind) : base(name, width, height, gamegrind)
        {
            _type = type;
            _range = range;
        }
        public Type Type
        {
            get { return _type; }
        }
        public override bool Operate()
        {
            if (!CheckCanOperate(-4))
            {
                return false;
            }

            System.Diagnostics.Debug.WriteLine(Name + " is scanning");
            Console.WriteLine("PPOOPO");

            Circle scanArea = new Circle();
            scanArea.Center = (Owner as Rover).Center;
            scanArea.Radius = GameGrid.CellSize * _range;

            List<GameObject> scanedGOs = GameGrid.GetScannedGameObjects(scanArea);

            foreach (GameObject scanGO in scanedGOs.FindAll(x => x.GetType() == _type))
            {
                if (scanGO != Owner)
                {
                    GameGrid.Level.AddScanObeject(scanGO.Center);
                }
            }

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
