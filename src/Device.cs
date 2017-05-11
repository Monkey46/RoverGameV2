using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverGameV2
{
    public abstract class Device : GameObject , IAttachable
    {
        private Battery _connectedbattery;
        private GameGrid _gamegrind;
        private IIsOwener _owner;
		// @Paul THe Chain Should end at the top Or bottom when i think about it idk ask matt
        public Device(string name, float width, float height, GameGrid gamegrid) : this (name, width, height, gamegrid, gamegrid)
        {
           // _gamegrind = gamegrind;
           // _owner = gamegrind;
        }
        public Device(string name, float width, float height, GameGrid gamegrind, IIsOwener owner) : base(name,width,height)
        {
            _gamegrind = gamegrind;
            _owner = owner;
        }
        public IIsOwener Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        public Battery ConnectedBattery
        {
            get { return _connectedbattery; }
            set { _connectedbattery = value; }
        }
        public GameGrid GameGrid
        {
            get { return _gamegrind; }
        }
        public bool CheckBattery()
        {
            if (ConnectedBattery == null)
            {
				// @Paul Printing to who?
                Console.WriteLine(Name + " has no Batteries connected");
                return false;
            }
            return true;
        }

        public abstract bool Operate();
    }
}
