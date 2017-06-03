using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
	/// <summary>
	/// This class will handle any Collision processing that needs to be done 
	/// </summary>
	public class ColsionProcessor
    {
        public ColsionProcessor()
        {

        }
		/// <summary>
		/// This will return and List of game objects that have collided with the given circle and the given list of Game objects 
		/// </summary>
		/// <param name="scanArea"></param>
		/// <param name="GOToScan"></param>
		/// <returns></returns>
		public List<GameObject> ScanColsions(Circle scanArea, List<GameObject> GOToScan)
        {
            List<GameObject> scannedGameObjects = new List<GameObject>();
            foreach (GameObject GO in GOToScan)
            {
                if (SwinGame.CircleRectCollision(scanArea, GO.HitBox))
                {
                    scannedGameObjects.Add(GO);
                }
            }
            return scannedGameObjects;
        }
		/// <summary>
		/// This will return and List of Specimens  that have collided with the given circle and the given list of Game objects 
		/// </summary>
		/// <param name="drillArea"></param>
		/// <param name="GOToDrill"></param>
		/// <returns></returns>
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
		/// <summary>
		/// This checks if GameObject 1 hasHas collided with GameObject 2
		///	and if tells GameObject 1 that it has collided with GameObject 2 
		/// </summary>
		/// <param name="GO1"></param>
		/// <param name="GO2"></param>
		public void MovmentCollisions(GameObject GO1, GameObject GO2)
        {
            if (SwinGame.RectanglesIntersect(GO1.HitBox, GO2.HitBox))
            {
                GO1.HasCollided(GO2);
            }
        }
		/// <summary>
		/// With the given list of GameObjects it will detect if any GameObject has collided with any other game object within the list 
		/// </summary>
		/// <param name="cellList"></param>
		public void DetectColsions(List<GameObject> cellList)
        {
            foreach (GameObject GO1 in cellList)
            {
                foreach (GameObject GO2 in cellList)
                {
                    if (GO1 != GO2)
                    {
                        MovmentCollisions(GO1, GO2);
                    }
                }
            }
        }
    }
}