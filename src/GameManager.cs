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
		Level _level;
		GUI _gui;
		GameGrid _grid;

		public GameManager()
		{
			SwinGame.OpenGraphicsWindow("GameMain", 900, 600);
			_grid = new GameGrid(20, 20, 30);
			_level = new Level(_grid);
			_gui = new GUI(_grid);
			_grid.Level = _level;
			Rover rover1 = MakeRover("TX9550");
			Rover rover2 = MakeRover("TX9441");

			_grid.SelectedRover = rover1;
			_level.SelectedGameObject = rover1;

			_level.LevelGameObjects.Add(rover1);
			_level.LevelGameObjects.Add(rover2);

			_level.LevelGameObjects.AddRange(MakeSpecimans());
			RandomizeLaction(_level.LevelGameObjects);


		}
		public void Loop()
		{
			SwinGame.ProcessEvents();
			SwinGame.ClearScreen(Color.White);
			// game Loop Goes here?
			_gui.HandleInput();
			_level.InputHandler();
			_level.Handlecollisions();
			_level.Update();
			_gui.Update();
			_gui.Render();
			_level.Render();
			SwinGame.DrawFramerate(0, 0);
			SwinGame.RefreshScreen(60);
		}

		private Rover MakeRover(string roverName)
		{
			Rover rover = new Rover(roverName, 20, 10, _grid);
			Motor motor1 = new Motor("Basic Motor", 10, 5, 2, _grid);
			Motor motor3 = new Motor("Basic Motor", 10, 5, 2, _grid);
			Battery bat1 = new Battery("Big Battery", 10, 5, 500);
			SolarPanel sp1 = new SolarPanel("SolarPanel", 10, 5, 1, _grid);
			Drill drill1 = new Drill("Drill", 10, 5, 10, _grid);
			Radar radar1 = new Radar("Radar", 10, 8, typeof(Specimen), 5, _grid);
			Camera camera1 = new Camera("Camera", 10, 8, 2, _grid);
			rover.Attach(bat1);
			rover.Attach(motor1);
			rover.Attach(motor3);
			rover.Attach(sp1);
			rover.Attach(drill1);
			rover.Attach(radar1);
			rover.Attach(camera1);
			return rover;
		}
		private List<GameObject> MakeSpecimans()
		{
			List<GameObject> specimans = new List<GameObject>();
			Specimen spec = new Specimen("Jacques", 5, 5, 3, Color.Purple);
			specimans.Add(spec);
			Specimen spec2 = new Specimen("Noonium", 10, 10, 1, Color.OliveDrab);
			specimans.Add(spec2);
			Specimen spec3 = new Specimen("Tienium", 10, 10, 20017, Color.Red);
			specimans.Add(spec3);
			Specimen spec4 = new Specimen("Maddite", 5, 5, 1, Color.HotPink);
			specimans.Add(spec4);
			Specimen spec5 = new Specimen("Andinium", 5, 5, 5, Color.IndianRed);
			specimans.Add(spec5);
			Specimen spec6 = new Specimen("Cool Beanium", 5, 5, 1, Color.LimeGreen);
			specimans.Add(spec6);
			Specimen spec7 = new Specimen("Paul's Remains", 5, 5, 1, Color.MintCream);
			specimans.Add(spec7);
			Specimen spec8 = new Specimen("Meth", 5, 5, 1, Color.PaleTurquoise);
			specimans.Add(spec8);
			Specimen spec9 = new Specimen("Red Mist", 5, 5, 1, Color.Peru);
			specimans.Add(spec9);
			Specimen spec10 = new Specimen("¿", 5, 5, 3242452, Color.Yellow);
			specimans.Add(spec10);
			return specimans;
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
