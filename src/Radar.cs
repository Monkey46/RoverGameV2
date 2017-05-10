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
        int _type;
        public Radar(string name, float width, float height,GameGrid gamegrind, int type) : base(name, width, height, gamegrind)
        {
            _type = type;
        }

        public int Type
        {
            get { return _type; }
        }
        public override bool Operate()
        {
            if (!CheckBattery())
            {
                Console.WriteLine(Name + " has no Battery conected");
                return false;
            }
            if (!ConnectedBattery.ChangePower(-4))
            {
                Console.WriteLine(ConnectedBattery.Name + " doesn't have enough charge to power " + Name);
                return false;
            }
            // Rover location 
            Cell roverLoction = GameGrid.FindGameObjectLocation(Owner as GameObject);
            int roverX = GameGrid.GetCellX(roverLoction);
            int roverY = GameGrid.GetCellY(roverLoction);
            // VV LATER Added Feture scalable radar VV
            // https://en.wikipedia.org/wiki/Midpoint_circle_algorithm 
            // ^^                                                   ^^
            /* r=rover
                 rx-5|rx-4|rx-3|rx-2|rx-1|rx  |rx+1|rx+2|rx+3|rx+4|rx+5|
            ry-5|    |    |    |  x |  x |  x |  x |  x |    |    |    |  rx-+2  5
            ry-4|    |    |  x |    |    |  x |    |    | x  |    |    |  rx-+3  7
            ry-3|    |  x |    |    |    |  x |    |    |    |  x |    |  rx-+4  9
            ry-2|  x |    |    |    |    |  x |    |    |    |    | x  |  rx-+5  11 loop 1
            ry-1|  x |    |    |    |    |  x |    |    |    |    | x  |  rx-+5  11 2
            ry  |  x |  x | x  | x  |  x |  x | x  | x  | x  | x  | x  |  rx-+5  11 3
            ry+1|  x |    |    |    |    |  x |    |    |    |    | x  |  rx-+5  11 4
            ry+2|  x |    |    |    |    |  x |    |    |    |    | x  |  rx-+5  11 5
            ry+3|    |  x |    |    |    |  x |    |    |    |  x |    |  rx-+4  9
            ry+4|    |    |  x |    |    |  x |    |    |  x |    |    |  rx-+3  7
            ry+5|    |    |    |  x |  x |  x |  x |  x |    |    |    |  rx-+2  5

            Total scaned = 97
            */
            Cell[][] tempgrid = GameGrid.Cells;
            Dictionary<Specimen, Cell> scanedSpecimens = new Dictionary<Specimen, Cell>();
            int count = 2;
            int loop = 0;
            for (int y = roverY - 5; y <= roverY + 5; y++)
            {
                for (int x = roverX - count; x <= roverX + count; x++)
                {
                    if (x>=0 && x<GameGrid.Width && y>=0 && y<GameGrid.Height)
                    {
                        Specimen tempSpec = tempgrid[x][y].Contents.Find(s => s is Specimen) as Specimen;
                        if (tempSpec != null)
                        {
                            scanedSpecimens.Add(tempSpec, tempgrid[x][y]);
                        }
                    }
                }
                if (loop == 4) count--;
                else if (count == 5) loop++;
                else count++;
            }
            // Display What The radar found!
            if (scanedSpecimens.Count <= 0)
            {
                Console.WriteLine("No Specimens found");
                return true;
            }
            string result = "Specimens Found:\n";
            foreach (KeyValuePair<Specimen, Cell> spec in scanedSpecimens)
            {
                result += "--------------------\n";
                switch (_type)
                {
                    case 3:
                        // return Spec name
                        result += "Name: " + spec.Key.Name + "\n";
                        goto case 2;
                    case 2:
                        // return Spec size
                        result += "    Size: " + spec.Key.Size + "\n";
                        goto case 1;
                    case 1:
                        // return Spec Laction
                        result += "    Location: [" + GameGrid.GetCellX(spec.Value) + "][" + GameGrid.GetCellY(spec.Value) + "]\n";
                        break;
                }
                

            }
            Console.WriteLine(result);
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
