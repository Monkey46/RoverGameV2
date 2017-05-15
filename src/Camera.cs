using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class Camera : Device
    {
        float _range;
        public Camera(string name, float range, GameGrid gamegrid) : base(name,gamegrid)
        {
            _range = range * gamegrid.CellSize;
        }
        public Camera(string name, float width, float height, float range, GameGrid gamegrid) : base(name, width, height, gamegrid)
        {
            _range = range * gamegrid.CellSize;
        }
        public override bool Operate()
        {
            if (!CheckCanOperate(0))
            {
                return false;
            }

            Circle ViewArea = new Circle();
            ViewArea.Center = (Owner as Rover).Center;
            ViewArea.Radius = _range;
            SwinGame.DrawCircle(Color.Blue, ViewArea);
             List<GameObject>viewedGameObjects = GameGrid.GetScannedGameObjects(ViewArea);
            GameGrid.Level.RenderList = viewedGameObjects;

            return true;

        }
        public override void Render()
        {
            
        }
        public override void Update()
        {

        }
    }
}
