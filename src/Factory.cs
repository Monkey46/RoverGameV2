using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class Factory
	{

		public Rover MakeRover(string roverName, GameGrid grid)
		{
			Rover rover = new Rover(roverName, 20, 10, grid);
			Motor motor1 = new Motor("Basic Motor", 10, 5, 2, grid);
			Motor motor3 = new Motor("Basic Motor", 10, 5, 2, grid);
			Battery bat1 = new Battery("Big Battery", 10, 5, 500);
			SolarPanel sp1 = new SolarPanel("SolarPanel", 10, 5, 1, grid);
			SolarPanel sp2 = new SolarPanel("Super SolarPanel", 10, 5, 3, grid);
			Drill drill1 = new Drill("Drill", 10, 5, 10, grid);
			Radar radar1 = new Radar("Radar", 10, 8, typeof(Specimen), 5, grid);
			Camera camera1 = new Camera("Camera", 10, 8, 2, grid);
			rover.Attach(sp2);
			rover.Attach(bat1);
			rover.Attach(motor1);
			rover.Attach(motor3);
			rover.Attach(sp1);
			rover.Attach(drill1);
			rover.Attach(radar1);
			rover.Attach(camera1);
			return rover;
		}
		public List<GameObject> MakeSpecimans(GameGrid grid)
		{
			List<GameObject> specimans = new List<GameObject>();
			Specimen spec = new Specimen("Jacques", 5, 5, 3, Color.Purple,grid);
			specimans.Add(spec);
			Specimen spec2 = new Specimen("Noonium", 10, 10, 1, Color.OliveDrab, grid);
			specimans.Add(spec2);
			Specimen spec3 = new Specimen("Tienium", 10, 10, 20017, Color.Red, grid);
			specimans.Add(spec3);
			Specimen spec4 = new Specimen("Maddite", 5, 5, 1, Color.HotPink, grid);
			specimans.Add(spec4);
			Specimen spec5 = new Specimen("Andinium", 5, 5, 5, Color.IndianRed, grid);
			specimans.Add(spec5);
			Specimen spec6 = new Specimen("Cool Beanium", 5, 5, 1, Color.LimeGreen, grid);
			specimans.Add(spec6);
			Specimen spec7 = new Specimen("Paul's Remains", 5, 5, 1, Color.MintCream, grid);
			specimans.Add(spec7);
			Specimen spec8 = new Specimen("Meth", 5, 5, 1, Color.PaleTurquoise, grid);
			specimans.Add(spec8);
			Specimen spec9 = new Specimen("Red Mist", 5, 5, 1, Color.Peru, grid);
			specimans.Add(spec9);
			Specimen spec10 = new Specimen("¿", 5, 5, 3242452, Color.Yellow, grid);
			specimans.Add(spec10);
			return specimans;
		}
	}
}
