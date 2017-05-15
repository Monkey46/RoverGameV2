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
        Cell[][] _cells;
        int _width, _height;
        Rover _selectedRover;
        float _cellSize;
        private Level _level;

        public GameGrid(int width, int height, float cellSize)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _cells = new Cell[width][];
            for (int x = 0; x < height; x++)
            {
                _cells[x] = new Cell[height];
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
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
            get { return _width; }
        }
        public int NumberOfYCells
        {
            get { return _height; }
        }
		public float Width
		{
			get { return _width * CellSize; }
		}
		public float Height
		{
			get { return _height * CellSize; }
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

        public int GetCellX(Cell getCell)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (getCell == _cells[i][j])
                    {
                        return i;
                    }
                }

            }
            return -1;
        }
        public int GetCellY(Cell getCell)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (getCell == _cells[i][j])
                    {
                        return j;
                    }
                }

            }
            return -1;
        }
        public Cell FindGameObjectLocation(GameObject findGO)
        {
            foreach (Cell[] cellA in _cells)
            {
                foreach (Cell singleCell in cellA)
                {
                    if (singleCell.Contents.Contains(findGO))
                    {
                        return singleCell;
                    }
                }
            }
            return null;
        }
        public List<GameObject> GetScannedGameObjects(Circle scanArea)
        {
             return _level.Colsions.ScanColsions(scanArea, _level.LevelGameObjects);
        }
        public List<Specimen> GetDrilledSpecimen(Circle drillArea)
        {
            return Level.Colsions.DrillColsions(drillArea, Level.LevelGameObjects);
        }
        public void Reder()
        {
            SwinGame.FillRectangle(Color.SandyBrown, 0, 0, NumberOfXCells * CellSize, NumberOfYCells * CellSize);
            // SwinGame.DrawBitmap(,);

            for (float xline = CellSize; xline <= NumberOfXCells * CellSize; xline = xline + CellSize)
            {
                SwinGame.DrawLine(Color.Black, xline, 0, xline, NumberOfYCells * CellSize);
            }
            for (float yline = CellSize; yline <= NumberOfYCells * CellSize; yline = yline + CellSize)
            {
                SwinGame.DrawLine(Color.Black, 0, yline, NumberOfXCells * CellSize, yline);
            }
        }
        /* Redunt Code from console based game
        public string FindCoordinatesOfGO(GameObject item)
        {
            Cell temp = FindGameObjectLocation(item);
            return "["+GetCellX(temp).ToString() +"]["+ GetCellY(temp).ToString()+"]";
        }

        
        public GameObject FindGameObject(string text)
        {
            foreach (Cell[] cellA in _cells)
            {
                foreach (Cell singleCell in cellA)
                {
                     foreach(GameObject go in singleCell.Contents)
                    {
                        if (go.AreYou(text))
                        {
                            return go;
                        }
                    }
                }
            }
            return null;
        }
        */
    }
}
