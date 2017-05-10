using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class ColsionManager
    {
        public ColsionManager()
        {

        }
        public void ScanColsions(Rectangle scanArea, List<GameObject> GOToScan)
        {
            foreach (GameObject GO in GOToScan)
            {
                if (SwinGame.RectanglesIntersect(scanArea, GO.HitBox ))
                {
                    GO.HasBeenScaned();
                }
            }
        }
        /*
        public List<GameObject> DrillColsions(Rectangle drillarea, List<GameObject> GOToDrill)
        {
            
            return null;
        }
        */
        public void MovmentCollisions(GameObject GO1, GameObject GO2)
        {
            if (SwinGame.RectanglesIntersect(GO1.HitBox,GO2.HitBox))
            {
                GO1.HasCollided(GO2);
            }
        }

    }
}
