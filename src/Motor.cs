﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class Motor : Device
    {
        private Direction _direction;
        private float _maxSpeed;

        public Motor(string name, float width, float height,float maxspeed,GameGrid gamegrind) : base(name, width,height, gamegrind)
        {
            _direction = Direction.none;
            _maxSpeed = maxspeed;
        }
        public Direction Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public override bool Operate()
        {
            if (!CheckCanOperate(-1))
            {
                return false;
            }
            //move rover
            /*
            Cell currentLaction = GameGrid.FindGameObjectLocation(Owner as GameObject);
            Cell[][] GameCells = GameGrid.Cells;
            int currX = GameGrid.GetCellX(currentLaction);
            int currY = GameGrid.GetCellY(currentLaction);
            */
            switch (_direction)
            {
                case Direction.up:
                    (Owner as Rover).YVelocity += -_maxSpeed;
                    break;
                case Direction.right:
                    (Owner as Rover).XVelocity += _maxSpeed;
                    break;
                case Direction.down:
                    (Owner as Rover).YVelocity += _maxSpeed;
                    break;
                case Direction.left:
                    (Owner as Rover).XVelocity += -_maxSpeed;
                    break;
            }
            return true;
        }
        public override void Render()
        {
            SwinGame.FillRectangle(Color.DeepPink, X, Y, Width, Height);
        }

        public override void Update()
        {

        }

    }
}
