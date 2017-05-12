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
        public GameManager()
        {
			// @Paul You already know to split this up
            GameGrid grid = new GameGrid(20, 20, 30);
            _level = new Level(grid);
            grid.Level = _level;
            //Open the game window
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);
            //SwinGame.ShowSwinGameSplashScreen();
            Rover rover1 = new Rover("Rover 1", 20, 10, grid);
            Motor motor1 = new Motor("Motor", 10, 5, 2, grid);
            Motor motor3 = new Motor("Motor", 10, 5, 2, grid);
            Battery bat1 = new Battery("Battery", 10, 5, 500);
            SolarPanel sp1 = new SolarPanel("SolarPanel", 10, 5, grid);
            Drill drill1 = new Drill("Drill",10,5,10,grid);
            Radar radar1 = new Radar("Radar", 10, 8, typeof(Specimen), grid);
            Rover rover2 = new Rover("MaddX5", 20, 10, grid);
            Motor motor2 = new Motor("Motor", 10, 5, 2, grid);
            Battery bat2 = new Battery("Battery", 10, 5, 200);
            SolarPanel sp2 = new SolarPanel("SolarPanel", 10, 5, grid);
            Specimen spec = new Specimen("Jacques", 5, 5, 3);
            spec.X = 300;
            spec.Y = 300;
            grid.SelectedRover = rover1;
            _level.SelectedGameObject = rover1;
            rover2.Attach(bat2);
            rover2.Attach(motor2);
            rover2.Attach(sp2);
            rover1.Attach(bat1);
            rover1.Attach(motor1);
            rover1.Attach(motor3);
            rover1.Attach(sp1);
            rover1.Attach(drill1);
            rover1.Attach(radar1);
            rover1.X = 50;
            rover1.Y = 60;
            rover2.X = 500;
            rover2.Y = 80;
            _level.LevelGameObjects.Add(rover1);
            _level.LevelGameObjects.Add(rover2);
            _level.LevelGameObjects.Add(spec);
        }
        public void Loop()
        {
            SwinGame.ProcessEvents();
            SwinGame.ClearScreen(Color.White);
            // game Loop Goes here?
            _level.InputHandler();
            _level.Handlecollisions();
            _level.Update();
            _level.Render();
            SwinGame.DrawFramerate(0, 0);
            SwinGame.RefreshScreen(60);
        }
    }
}
