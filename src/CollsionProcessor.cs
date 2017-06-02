﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace RoverGameV2
{
    public class ColsionProsser
    {
        public ColsionProsser()
        {

        }
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
            if (SwinGame.RectanglesIntersect(GO1.HitBox, GO2.HitBox))
            {
                GO1.HasCollided(GO2);
            }
        }
		public void WallCollisions(List<GameObject> sideCells)
		{
			foreach (GameObject iGO in sideCells)
			{
				//
			}
		}
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