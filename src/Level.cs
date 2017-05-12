using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class Level
    {
        private GameGrid _gamegrid;
        private ColsionManager _colsionManager;
        private List<GameObject> _levelList;
        private GameObject _selectedGO;
        private Dictionary<Point2D,int> _scanedGameObjects;
        private int _gameTick;
        private int _scanDuration;

        public Level(GameGrid gamegrid)
        {
            _gamegrid = gamegrid;
            _colsionManager = new ColsionManager();
            _levelList = new List<GameObject>();
            _scanedGameObjects = new Dictionary<Point2D,int>();
            _gameTick = 0;
            _scanDuration = 300;
        }
        public List<GameObject> LevelGameObjects
        {
            get { return _levelList; }
            set { _levelList = value; }
        }
        public GameObject SelectedGameObject
        {
            get { return _selectedGO; }
            set { _selectedGO = value; }
        }
        public ColsionManager Colsions
        {
            get { return _colsionManager; }
        }
        public void InputHandler()
        {
            //move Get the Motor to do the move ment
            if (SwinGame.KeyDown(KeyCode.UpKey) || SwinGame.KeyDown(KeyCode.WKey))
            {
                _gamegrid.SelectedRover.Move(Direction.up);
            }
            if (SwinGame.KeyDown(KeyCode.DownKey) || SwinGame.KeyDown(KeyCode.SKey))
            {
                _gamegrid.SelectedRover.Move(Direction.down);
            }
            if (SwinGame.KeyDown(KeyCode.RightKey) || SwinGame.KeyDown(KeyCode.DKey))
            {
                _gamegrid.SelectedRover.Move(Direction.right);
            }
            if (SwinGame.KeyDown(KeyCode.LeftKey) || SwinGame.KeyDown(KeyCode.AKey))
            {
                _gamegrid.SelectedRover.Move(Direction.left);
            }
            // Charge with SolarPanel
            if (SwinGame.KeyDown(KeyCode.SpaceKey))
            {
                _gamegrid.SelectedRover.Charge();
            }
            //Select GameObject and Change Selected rover
            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
				/*
					@Paul Im Guessing sGO is Selected Game Object but kinda unclear 
					Because then you itrate though each one and there is a _selectedGO?
                    DONE
				*/ 
                foreach (GameObject GO in _levelList)
                {
                    if (GO.IsAt(SwinGame.MousePosition()))
                    {
                        _selectedGO = GO;
                        if (GO is Rover)
                        {
                            _gamegrid.SelectedRover = GO as Rover;
                        }
                    }
                }
            }
            // Drill
            if (SwinGame.KeyTyped(KeyCode.EKey))
            {
                _gamegrid.SelectedRover.Drill();
            }
            // Scan
            if (SwinGame.KeyTyped(KeyCode.VKey))
            {
                _gamegrid.SelectedRover.Scan();
            }
            // Drop and Pick up (need gui)
            if (SwinGame.MouseClicked(MouseButton.RightButton))
            {
				/* 
					@Paul So there is _selectedGO and SelectedGameObject? You should also be using elseware in the code
                */
				if (!(SelectedGameObject is Rover))
                {
                    // Check is rover is nearby 
                    // if so get rover to Attach or Collect it.
                }
            }
            // Connecting and Discounting Devices to Batteries 
        }
        public void Update()
        {
            foreach (GameObject iGO in _levelList)
            {
                iGO.Update();
            }
            _gameTick++;
        }
        public void Handlecollisions()
        {   
            _colsionManager.DetectColsions(_levelList);
        }
        public void Render()
        {
            _gamegrid.Reder();
            _selectedGO.RederOutline();
            foreach (GameObject iGO in _levelList)
            {
                iGO.Render();
            }
            RenderScan();
        }
        public void Pickup(GameObject moveGO)
        {
            _gamegrid.SelectedRover.Attach(moveGO as Device);
            _levelList.Remove(moveGO);

        }
        public void Drop(GameObject moveGO)
        {
            _gamegrid.SelectedRover.Detach(moveGO as Device);
            _levelList.Add(moveGO);
        }
        public void AddScanObeject(Point2D addCordanite)
        {
            _scanedGameObjects.Add(addCordanite,_gameTick);
        }
        private void RenderScan()
        {
            List<Point2D> deletelist = new List<Point2D>();
            foreach (KeyValuePair<Point2D,int> scanKVP in _scanedGameObjects)
            {
                if (_gameTick >= scanKVP.Value + _scanDuration)
                {
                    deletelist.Add(scanKVP.Key);
                }
                else
                {
                    
                    SwinGame.FillCircle(Color.LightGreen, scanKVP.Key.X,scanKVP.Key.Y,4);
                }
            }
            foreach (Point2D i in deletelist)
            {
                _scanedGameObjects.Remove(i);
            }

        }
		// @Paul How is this Seprate from HandleCollsions?
        private void UpdateColsionList()
        {
            
            // @Task Build Later if need better Colsions handler 

        }
    }
}

