﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    class Level
    {
        GameGrid _gamegrid;
        ColsionManager _colsionM;
        private List<GameObject> _levelList;
        GameObject _selectedGO;

        public Level(GameGrid gamegrid)
        {
            _gamegrid = gamegrid;
            _colsionM = new ColsionManager();
            _levelList = new List<GameObject>();
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
                foreach (GameObject sGO in _levelList)
                {
                    if (sGO.IsAt(SwinGame.MousePosition()))
                    {
                        _selectedGO = sGO;
                        if (sGO is Rover)
                        {
                            _gamegrid.SelectedRover = sGO as Rover;
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
            foreach (GameObject GO in _levelList)
            {
                GO.Update();
            }
        }
        public void Handlecollisions()
        {   
            DetectColsions();
        }
        public void Render()
        {
            _gamegrid.Reder();
            _selectedGO.RederOutline();
            foreach (GameObject GO in _levelList)
            {
                GO.Render();
            }
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
        public void UpdateColsionList()
        {
            // Build Later if need better Colsions handler 
        }
        public void DetectColsions()
        {
            foreach (GameObject GO1 in _levelList)
            {
                foreach (GameObject GO2 in _levelList)
                {
                    if (GO1 != GO2)
                    {
                        _colsionM.MovmentCollisions(GO1, GO2);
                    }
                }
            }
        }
    }
}
