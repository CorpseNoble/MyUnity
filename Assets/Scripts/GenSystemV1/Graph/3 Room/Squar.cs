using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public class Squar : Room
    {
        public override void Generate()
        {
            base.Generate();
            Vector3 currentPos = transform.position;
            var rightVector = buildVector.ToRight();
            var leftVector = buildVector.ToLeft();
            var generalList = new List<GraphElement>();

            for (int j = 0; j < roomSize; j++)
            {
                if (blacklist.CheckPos(currentPos))
                {
                    if (interestPlace == null)
                        interestPlace = generalList.Last() as Point;
                    break;

                }

                var se = FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector);
                generalList.Add(se);

                if (j == roomSize / 2)
                    interestPlace = generalList.Last() as Point;

                if (j == roomSize - 1)
                    newWays.Add(new NewWay(generalList.Last(), generalList.Last().transform.position.StepH(buildVector), buildVector));

                Vector3 currentPosL, currentPosR;
                currentPosL = currentPosR = currentPos;
                for (int i = 0; i < roomSize / 2 - 1; i++)
                {
                    currentPosR = currentPosR.StepH(rightVector);
                    if (blacklist.CheckPos(currentPosR))
                        break;
                    var pr = FabricGameObject.InstantiateElement<Point>(currentPosR, this, buildVector);
                    subElements.Add(pr);
                }

                for (int i = 0; i < roomSize / 2 - 1; i++)
                {
                    currentPosL = currentPosL.StepH(leftVector);
                    if (blacklist.CheckPos(currentPosL))
                        break;
                    var pl = FabricGameObject.InstantiateElement<Point>(currentPosL, this, buildVector);
                    subElements.Add(pl);
                }

                currentPos = currentPos.StepH(buildVector);

            }
            subElements.AddRange(generalList);
            rootElement = generalList[0];
           
            GenPointEntry();
            GenNewWays();
          
        }
    }

}
