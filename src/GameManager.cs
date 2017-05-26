using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class GameManager
	{
		private Level _level;
		private GUI _gui;
		private GameGrid _grid;
		private Factory _factory;

		public GameManager()
		{
			SwinGame.OpenGraphicsWindow("GameMain", 900, 600);
			_grid = new GameGrid(20, 20, 30);
			_level = new Level(_grid);
			_gui = new GUI(_grid);
			_grid.Level = _level;
			_factory = new Factory();

			Rover rover1 = _factory.MakeRover("TX9550", _grid);
			Rover rover2 = _factory.MakeRover("TX9441",_grid);

			_grid.SelectedRover = rover1;
			_level.SelectedGameObject = rover1;

			_level.LevelGameObjects.Add(rover1);
			_level.LevelGameObjects.Add(rover2);

			_level.LevelGameObjects.AddRange(_factory.MakeSpecimans(_grid));

			RandomizeLaction(_level.LevelGameObjects);
		}
		public void Loop()
		{
			SwinGame.ProcessEvents();
			SwinGame.ClearScreen(Color.White);
			_gui.HandleInput();
			_level.InputHandler();
			_level.Handlecollisions();
			_level.Update();
			_gui.Update();
			_level.Render();
			_gui.Render();
			SwinGame.DrawFramerate(0, 0);
			SwinGame.RefreshScreen(60);
		}


		private void RandomizeLaction(List<GameObject> GameObjects)
		{
			Random rand = new Random();
			foreach (GameObject iGO in GameObjects)
			{

				iGO.X = rand.Next(1, (int)(_grid.Width - iGO.Width));
				iGO.Y = rand.Next(1, (int)(_grid.Height - iGO.Height));
			}
		}
	}
}
