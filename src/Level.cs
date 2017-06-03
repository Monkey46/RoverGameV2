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
		private ColsionProcessor _colsionProcessor;

		/// <summary>
		/// List of all The Game Objects in the Game
		/// </summary>
		private List<GameObject> _levelList;

		/// <summary>
		/// List of GameObjects The camera sees and is rendered
		/// </summary>
		private List<GameObject> _renderList;

		private GameObject _selectedGO;
		/// <summary>
		/// A List of scan object loactions and the time it was scanned
		/// </summary>
		private List<Tuple<Point2D, int>> _scanedGameObjects;

		/// <summary>
		/// each fram the GameTick in increase 
		/// </summary>
		private int _gameTick;

		/// <summary>
		///  How the long in frames that the sacn Object will alive for
		/// </summary>
		private int _scanDuration;

		private Random _randomer;

		public Level(GameGrid gamegrid)
		{
			_gamegrid = gamegrid;
			_colsionProcessor = new ColsionProcessor();
			_levelList = new List<GameObject>();
			_scanedGameObjects = new List<Tuple<Point2D, int>>();
			_gameTick = 0;

			// 120 frams at 60fps = 2secs
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
		public ColsionProcessor ColsionProcessor
		{
			get { return _colsionProcessor; }
		}
		public Random Randomer
		{
			get { return _randomer; }
		}

		/// <summary>
		///  Process input from user
		/// </summary>
		public void InputHandler()
		{
			// Move the Rover
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
			// Charge with SolarPanels
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
		/// <summary>
		/// Will do this every frame 
		/// This will tell each game object to update form the level list and
		/// then Update the render list with the selected Rover's Camera
		/// incremented The game tick
		/// </summary>
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
			_colsionProcessor.DetectColsions(_levelList);
		}
		/// <summary>
		/// Renders all things that need to be rendered in the game 
		/// </summary>
		public void Render()
		{
			_gamegrid.Reder();
			RenderRenderList();
			RenderRovers();
			RenderScan();
		}

		/// <summary>
		///	Picks up a game object at as to the rover
		/// and remove it from the level list
		/// </summary>
		/// <param name="moveGO"></param>
		public void Pickup(GameObject moveGO)
		{
			_gamegrid.SelectedRover.Attach(moveGO as Device);
			_levelList.Remove(moveGO);

		}

		/// <summary>
		/// detaches  an object from The selected River and Add it to the level list
		/// </summary>
		/// <param name="moveGO"></param>
		public void Drop(GameObject moveGO)
		{
			_gamegrid.SelectedRover.Detach(moveGO as Device);
			_levelList.Add(moveGO);
		}

		/// <summary>
		/// Add the co-ordinate parameter and the current Game tick to the scanned game objects 
		/// </summary>
		/// <param name="addCordanite"></param>
		public void AddScanObeject(Point2D addCordanite)
		{
			_scanedGameObjects.Add(Tuple.Create<Point2D, int>(addCordanite, _gameTick));
		}

		/// <summary>
		/// Renders a small green circle at the location All the Coordinate in the scanned objects list
		///	and delete coordinates if it's been there longer than the scan duration 
		/// </summary>
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
		/// <summary>
		/// Clears the level’s render list
		/// </summary>
		public void ClearRenderList()
		{
			if (RenderList != null)
			{
				RenderList.Clear();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void UpdateColsionList()
		{

			// @Task Build Later if need better Colsions handler 

		}

		/// <summary>
		/// Will make each game object in the render list Render itself
		///	and if it's the selected object it'll make The Game object render it’s outline
		/// </summary>
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

		/// <summary>
		/// Makes every Rover in the level list render 
		/// </summary>
		private void RenderRovers()
		{
			foreach (Rover rover in _levelList.FindAll(x => x is Rover))
			{
				rover.Render();
			}
		}
	}
}

