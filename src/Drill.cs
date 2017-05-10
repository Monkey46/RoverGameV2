using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class Drill : Device
    {
        int _worn;
        public Drill(string name, float width, float height, GameGrid gamegrind) : base(name, width, height, gamegrind)
        {
            _worn = 0;
        }

        public int Worn
        {
            get { return _worn; }
        }
        public override bool Operate()
        {
            if (_worn >= 100)
            {
                Random rand = new Random();
                int chance = rand.Next(0, 100);
                if (chance <= 20)
                {
                    Console.WriteLine(Name +" failed to drill");
                    return false;
                }
            }
			// @Paul So it changes it to a GameObject from a cell When both times it's casted as a GameObject Why?
            Cell drillingcell = GameGrid.FindGameObjectLocation(Owner as GameObject);
            GameObject drilledGO = drillingcell.Contents.Find(x => x is Specimen);
            if (drilledGO == null)
            {
                _worn += 10;
                Console.WriteLine(Name + " drilled nothing");
                return false;
            }
            else
            {
                Console.WriteLine(Name + " drilled Successfully");
                (Owner as Rover).Collect(drilledGO as Specimen);
                drillingcell.Contents.RemoveAll(x => x == drilledGO);
                _worn += 5;
                return true;
            }

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

