using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	// @Paul So how does this relate to the Other Collison methods
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
        public List<Specimen> DrillColsions(Circle drillArea, List<GameObject> GOToDrill)
        {
            List<Specimen> drilledItems = new List<Specimen>();
            foreach (GameObject GO in GOToDrill)
            {
                if (SwinGame.CircleRectCollision(drillArea, GO.HitBox) && GO is Specimen)
                {
                    drilledItems.Add(GO as Specimen);
                }
            }
            return drilledItems;
        }
        public void MovmentCollisions(GameObject GO1, GameObject GO2)
        {
            if (SwinGame.RectanglesIntersect(GO1.HitBox,GO2.HitBox))
            {
                GO1.HasCollided(GO2);
            }
        }

    }
}
