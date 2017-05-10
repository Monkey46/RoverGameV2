using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	// @Paul What if this was a GameObject?
    public class GameGrid : IHasOwener
    {
        Cell[][] _cells;
        int _width, _height;
        Rover _selectedRover;
        float _cellSize;

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
        public int Width
        {
            get { return _width; }
        }
        public int Height
        {
            get { return _height; }
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
        public void Reder()
        {
            SwinGame.FillRectangle(Color.Brown, 0, 0, Width * CellSize, Height * CellSize);
            // SwinGame.DrawBitmap(,);

            for (float xline = CellSize; xline <= Width * CellSize; xline = xline + CellSize)
            {
                SwinGame.DrawLine(Color.Black, xline, 0, xline, Height * CellSize);
            }
            for (float yline = CellSize; yline <= Height * CellSize; yline = yline + CellSize)
            {
                SwinGame.DrawLine(Color.Black, 0, yline, Width * CellSize, yline);
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
