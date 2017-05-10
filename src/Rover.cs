using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class Rover : GameObject , IHasOwener
    {
        List<Battery> _batteries;
        List<Device> _devices;
        List<Specimen> _specimens;
        GameGrid _gamegrind;
        public Rover(string name, float width, float height, GameGrid gamegrind ) : base(name,width,height)
        {
            _batteries = new List<Battery>();
            _devices = new List<Device>();
            _specimens = new List<Specimen>();
            _gamegrind = gamegrind;
        }

        public List<Battery> Batteries
        {
            get { return _batteries; }
        }
        public List<Device> Devices
        {
            get { return _devices; }
        }
        public List<Specimen> Specimens
        {
            get { return _specimens; }
        }
        public override void Render()
        {
            SwinGame.FillRectangle(Color.Red, X, Y, Width, Height);
            SwinGame.DrawText("R",Color.Black,X+1,Y+1);
        }

        public override void Update()
        {
            X += XVelocity;
            Y += YVelocity;
            PreXVelocity = XVelocity;
            PreYVelocity = YVelocity;
            XVelocity = 0;
            YVelocity = 0;
        }
        public void Attach(IAttachable attachItem)
        {
            if (attachItem is Battery)
            {
                _batteries.Add(attachItem as Battery);
                /*
                Cell cell = _gamegrind.FindGameObjectLocation(attachItem as GameObject);
                if (cell != null)
                {
                    cell.Contents.RemoveAll(x => x == attachItem);
                }
                */
            }
            else
            {
                (attachItem as Device).Owner = this;
                _devices.Add(attachItem as Device);
                if (_batteries.Count != 0)
                {
                    // find highest battery power
                    Battery highestBat = _batteries.First();
                    foreach (Battery bat in _batteries)
                    {
                        if (highestBat.Power < bat.Power)
                        {
                            highestBat = bat;
                        }
                    }
                // connect attachitem to high bat
                (attachItem as Device).ConnectedBattery = highestBat;
                }
                /*
                Cell cell = _gamegrind.FindGameObjectLocation(attachItem as GameObject);
                if (cell != null)
                {
                    cell.Contents.RemoveAll(x => x == attachItem);
                }
                */
            }
        }
        public void Detach(IAttachable attachItem)
        {
            if (attachItem is Battery)
            {
                _batteries.RemoveAll(x => x == attachItem);
            }
            else
            {
                _devices.RemoveAll(x => x == attachItem);
                (attachItem as Device).Owner = _gamegrind;
            }
           // Console.WriteLine(Name+" has attached "+(attachItem as GameObject).Name);
        }
        public void Collect(Specimen newSpecimen)
        {
            _specimens.Add(newSpecimen);
            Console.WriteLine(Name+" has collected "+newSpecimen.Name + " weighing  "+newSpecimen.Size);
        }

        public bool Move(Direction direction)
        {
            if (_devices.Exists(x=> x is Motor))
            {
                    foreach (Motor motor in _devices.FindAll(m=> m is Motor))
                    {
                    motor.Direction = direction;
                    motor.Operate();
                    }
                return true;
            }
            return false;
        }
        public bool Charge()
        {
            if (_devices.Exists(x => x is SolarPanel))
            {
                foreach (SolarPanel Sp in _devices.FindAll(s => s is SolarPanel))
                {
                    Sp.Operate();
                }
                return true;
            }
            return false;
        }
        public bool Drill()
        {
            if (_devices.Exists(x => x is Drill))
            {
                foreach (Drill drill in _devices.FindAll(s => s is Drill))
                {
                    if (drill.Operate())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool Scan()
        {
            return false;
        }


    }
}
