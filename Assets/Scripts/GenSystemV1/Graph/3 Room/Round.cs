using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public class Round : Room
    {
        public override void Generate()
        {
            base.Generate();

            var rad = (roomSize - 1) / 2;
            Vector3 center = transform.position.StepH(buildVector, rad);
            Vector3 currentPos = transform.position;
            var rightVector = buildVector.ToRight();
            var leftVector = buildVector.ToLeft();
            var generalList = new List<GraphElement>();
            var HScale = FabricGameObject.elementsData.HorScale;
            for (int j = 0; j < roomSize; j++)
            {
                if (blacklist.CheckPos(currentPos))
                {
                    if (interestPlace == null)
                        interestPlace = generalList.Last() as Point;
                    break;
                }
                generalList.Add(FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector));
                if (currentPos == center)
                    interestPlace = generalList.Last() as Point;
                if (j == rad * 2)
                    newWays.Add(new NewWay(generalList.Last(), generalList.Last().transform.position.StepH(buildVector), buildVector));

                Vector3 currentPosL, currentPosR;
                currentPosL = currentPosR = currentPos;
                for (int i = 0; i < rad; i++)
                {
                    currentPosR = currentPosR.StepH(rightVector);
                    if (blacklist.CheckPos(currentPosR))
                        break;
                    if (Vector3.Distance(center, currentPosR) <= rad * HScale)
                    {
                        var er = FabricGameObject.InstantiateElement<Point>(currentPosR, this, buildVector);
                        subElements.Add(er);
                    }
                }

                for (int i = 0; i < rad; i++)
                {
                    currentPosL = currentPosL.StepH(leftVector);
                    if (blacklist.CheckPos(currentPosL))
                        break;

                    if (Vector3.Distance(center, currentPosL) <= rad * HScale)
                    {
                        var el = FabricGameObject.InstantiateElement<Point>(currentPosL, this, buildVector);
                        subElements.Add(el);
                    }
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
