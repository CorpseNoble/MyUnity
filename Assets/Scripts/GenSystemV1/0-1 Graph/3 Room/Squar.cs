using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public class Squar : Room
    {
        public override void Generate()
        {
            Vector3 currentPos = transform.position;
            var rightVector = buildVector.ToRight();
            var leftVector = buildVector.ToLeft();
            var roomLenght = (parentElement as Zone).roomSize;
            var roomSize = (parentElement as Zone).roomSize;
            var area = parentElement.parentElement as Area;
            var generalList = new List<GraphElement>();

            for (int j = 0; j < roomLenght; j++)
            {
                if (blacklist.Contains(currentPos))
                {
                    if (interestPlace == null)
                        interestPlace = generalList.Last() as Point;
                    break;

                }

                var se = FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector);
                generalList.Add(se);

                if (j == roomLenght / 2)
                    interestPlace = generalList.Last() as Point;

                if (j == roomLenght - 1)
                    newWays.Add((generalList.Last(), generalList.Last().transform.position + buildVector * HScale, buildVector));

                Vector3 currentPosL, currentPosR;
                currentPosL = currentPosR = currentPos;
                for (int i = 0; i < roomSize / 2 - 1; i++)
                {
                    currentPosR += rightVector * HScale;
                    if (blacklist.Contains(currentPosR))
                        break;
                    var pr = FabricGameObject.InstantiateElement<Point>(currentPosR, this, buildVector);
                    subElements.Add(pr);
                    if (j == roomLenght - 1 && i == roomSize / 2 - 1)
                    {
                        newWays.Add((pr, pr.transform.position + rightVector * HScale, rightVector));
                        newWays.Add((pr, pr.transform.position + buildVector * HScale, buildVector));
                    }
                    if (j == roomLenght - 1 && i == 0)
                    {
                        newWays.Add((pr, pr.transform.position + rightVector * HScale, rightVector));
                        newWays.Add((pr, pr.transform.position + rightVector.ToRight() * HScale, rightVector.ToRight()));
                    }
                }

                for (int i = 0; i < roomSize / 2 - 1; i++)
                {
                    currentPosL += leftVector * HScale;
                    if (blacklist.Contains(currentPosL))
                        break;
                    var pl = FabricGameObject.InstantiateElement<Point>(currentPosL, this, buildVector);
                    subElements.Add(pl);
                    if (j == roomLenght - 1 && i == roomSize / 2 - 1)
                    {
                        newWays.Add((pl, pl.transform.position + leftVector * HScale, leftVector));
                        newWays.Add((pl, pl.transform.position + buildVector * HScale, buildVector));
                    }
                    if (j == roomLenght - 1 && i == 0)
                    {
                        newWays.Add((pl, pl.transform.position + leftVector * HScale, leftVector));
                        newWays.Add((pl, pl.transform.position + leftVector.ToLeft() * HScale, leftVector.ToLeft()));
                    }

                }

                currentPos += buildVector * HScale;

            }
            subElements.AddRange(generalList);
            rootElement = generalList[0];
            if (backElement != null)
            {
                backElement.Connect(rootElement);
            }
            for (int i = 0; i < subElements.Count; i++)
            {
                GraphElement e = subElements[i];
                var d = e.transform.position.About(HScale);
                var aboutCons = from t in subElements
                                where d.Contains(t.transform.position)
                                select t;

                if (aboutCons.Count() == 0)
                {
                    subElements.RemoveAt(i);
                    i--;
                    continue;
                }
                foreach (var a in aboutCons)
                    e.Connect(a);

                e.Generate();
            }
            //if (interestPlace != null)
            //    GenRoomEntry();
            //else Debug.Log("interestPlace == Vector3.zero");
        }
    }

}
