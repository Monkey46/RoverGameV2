﻿using System;
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
		private ColsionProsser _colsionManager;
		private List<GameObject> _levelList;
		private List<GameObject> _renderList;
		private GameObject _selectedGO;
		private List<Tuple<Point2D, int>> _scanedGameObjects;
		private int _gameTick;
		private int _scanDuration;
		private Random _randomer;

		public Level(GameGrid gamegrid)
		{
			_gamegrid = gamegrid;
			_colsionManager = new ColsionProsser();
			_levelList = new List<GameObject>();
			_scanedGameObjects = new List<Tuple<Point2D, int>>();
			_gameTick = 0;
			_scanDuration = 120;
			_renderList = new List<GameObject>();
			_randomer = new Random();

		}
		public List<GameObject> LevelGameObjects
		{
			get { return _levelList; }
			set { _levelList = value; }
		}
		public List<GameObject> RenderList
		{
			get { return _renderList; }
			set { _renderList = value; }
		}
		public GameObject SelectedGameObject
		{
			get { return _selectedGO; }
			set { _selectedGO = value; }
		}
		public ColsionProsser Colsions
		{
			get { return _colsionManager; }
		}
		public Random Randomer
		{
			get { return _randomer; }
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
				_gamegrid.SelectedRover.ChargeBatteries();
			}
			//Select GameObject and Change Selected rover
			if (SwinGame.MouseClicked(MouseButton.LeftButton))
			{
				foreach (GameObject iGO in _levelList)
				{
					if (iGO.IsAt(SwinGame.MousePosition()))
					{
						_selectedGO = iGO;
						if (iGO is Rover)
						{
							_gamegrid.SelectedRover = iGO as Rover;
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
			// Pick up
			if (SwinGame.MouseClicked(MouseButton.RightButton))
			{
				foreach (GameObject iGO in _levelList)
				{
					if (iGO.IsAt(SwinGame.MousePosition()))
					{
						if (!(iGO is Rover))
						{
							// Check is rover is nearby 
							Circle nearSelectedRover = new Circle();
							nearSelectedRover.Center = _gamegrid.SelectedRover.Center;
							nearSelectedRover.Radius = _gamegrid.CellSize;
							if (SwinGame.CircleRectCollision(nearSelectedRover, iGO.HitBox))
							{
								if (iGO is IAttachable)
								{
									_gamegrid.Pickup(iGO);
								}
								return;
							}
						}
					}
				}
			}
		}
		public void Update()
		{
			foreach (GameObject iGO in _levelList)
			{
				iGO.Update();
			}
			_gamegrid.SelectedRover.UpdateRenderList();
			_gameTick++;
		}
		public void Handlecollisions()
		{
			_colsionManager.DetectColsions(_levelList);
		}
		public void Render()
		{
			_gamegrid.Reder();
			RenderRenderList();
			RenderRovers();
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
			_scanedGameObjects.Add(Tuple.Create<Point2D, int>(addCordanite, _gameTick));
		}
		private void RenderScan()
		{
			List<Tuple<Point2D, int>> deletelist = new List<Tuple<Point2D, int>>();
			foreach (Tuple<Point2D, int> scanTuple in _scanedGameObjects)
			{
				if (_gameTick >= scanTuple.Item2 + _scanDuration)
				{
					deletelist.Add(scanTuple);
				}
				else
				{
					SwinGame.FillCircle(Color.LightGreen, scanTuple.Item1.X, scanTuple.Item1.Y, 4);
				}
			}
			foreach (Tuple<Point2D, int> i in deletelist)
			{
				_scanedGameObjects.Remove(i);
			}

		}
		public void ClearRenderList()
		{
			if (RenderList != null)
			{
				RenderList.Clear();
			}
		}

		private void UpdateColsionList()
		{

			// @Task Build Later if need better Colsions handler 

		}

		private void RenderRenderList()
		{
			if (_renderList != null)
			{
				foreach (GameObject iGO in _renderList)
				{
					iGO.Render();
				}
				if (_renderList.Contains(_selectedGO))
				{
					_selectedGO.RederOutline();
				}
				else
				{
					_gamegrid.SelectedRover.RederOutline();
				}
			}
		}
		private void RenderRovers()
		{
			foreach (Rover rover in _levelList.FindAll(x => x is Rover))
			{
				rover.Render();
			}
		}
	}
}

