using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	public class GameGrid : IIsOwener
	{
		private Cell[][] _cells;
		private int _xNumberOfCells, _yNumberOfCells;
		private Rover _selectedRover;
		private float _cellSize;
		private Level _level;
		Bitmap background;

		public GameGrid(int xNumberOfCells, int yNumberOfCells, float cellSize)
		{
			_xNumberOfCells = xNumberOfCells;
			_yNumberOfCells = yNumberOfCells;
			_cellSize = cellSize;
			background = new Bitmap("outflow2.bmp");
			_cells = new Cell[xNumberOfCells][];
			for (int x = 0; x < yNumberOfCells; x++)
			{
			    _cells[x] = new Cell[yNumberOfCells];
			}
			
			for (int i = 0; i < xNumberOfCells; i++)
			{
			    for (int j = 0; j < yNumberOfCells; j++)
			    {
			        _cells[i][j] = new Cell();
			    }
			 }
		}
		public Cell[][] Cells
		{
			get { return _cells; }
		}
		public int NumberOfXCells
		{
			get { return _xNumberOfCells; }
		}
		public int NumberOfYCells
		{
			get { return _yNumberOfCells; }
		}
		public float Width
		{
			get { return _xNumberOfCells * CellSize; }
		}
		public float Height
		{
			get { return _yNumberOfCells * CellSize; }
		}
		public float CellSize
		{
			get { return _cellSize; }
			//set { _cellSize = value; }
		}
		public Rover SelectedRover
		{
			get { return _selectedRover; }
			set { _selectedRover = value; }
		}
		public Level Level
		{
			get { return _level; }
			set { _level = value; }
		}

		// @Task Should I clean this up??
		public List<GameObject> GetScannedGameObjects(Circle scanArea)
		{
			return _level.Colsions.ScanColsions(scanArea, _level.LevelGameObjects);
		}
		public List<Specimen> GetDrilledSpecimen(Circle drillArea)
		{
			return Level.Colsions.DrillColsions(drillArea, Level.LevelGameObjects);
		}
		public void Drop(GameObject dropGO)
		{
			if (dropGO is Specimen)
			{
				_selectedRover.Specimens.Remove(dropGO as Specimen);
			}
			else _selectedRover.Detach(dropGO as IAttachable);
			dropGO.X = _selectedRover.X;
			dropGO.Y = _selectedRover.Y;
			_level.LevelGameObjects.Add(dropGO);
		}
		public void Pickup(GameObject pickUpGO)
		{
			_selectedRover.Attach(pickUpGO as IAttachable);
			_level.LevelGameObjects.Remove(pickUpGO);
		}
		public void Reder()
		{
			//SwinGame.FillRectangle(Color.SandyBrown, 0, 0, NumberOfXCells * CellSize, NumberOfYCells * CellSize);
			SwinGame.DrawBitmap(background, 0, 0);

			for (float xline = CellSize; xline <= NumberOfXCells * CellSize; xline = xline + CellSize)
			{
				SwinGame.DrawLine(Color.Black, xline, 0, xline, NumberOfYCells * CellSize);
			}
			for (float yline = CellSize; yline <= NumberOfYCells * CellSize; yline = yline + CellSize)
			{
				SwinGame.DrawLine(Color.Black, 0, yline, NumberOfXCells * CellSize, yline);
			}
		}
		public void Checkbonders(GameObject go)
		{
			if (go.X2 > Width) go.X2 = Width - 1;
			if (go.X < 0) go.X = 1;
			if (go.Y2 > Height) go.Y2 = Height - 1;
			if (go.Y < 0) go.Y = 1;
		}
	}
}
