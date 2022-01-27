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
            Vector3 center = transform.position + buildVector * rad * HScale;
            Vector3 currentPos = transform.position;
            var rightVector = buildVector.ToRight();
            var leftVector = buildVector.ToLeft();
            var generalList = new List<GraphElement>();

            for (int j = 0; j < roomSize; j++)
            {
                if (blacklist.Contains(currentPos))
                {
                    if (interestPlace == null)
                        interestPlace = generalList.Last() as Point;
                    break;
                }
                generalList.Add(FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector));
                if (currentPos == center)
                    interestPlace = generalList.Last() as Point;
                if (j == rad * 2)
                    newWays.Add(new NewWay(generalList.Last(), generalList.Last().transform.position + buildVector * HScale, buildVector));

                Vector3 currentPosL, currentPosR;
                currentPosL = currentPosR = currentPos;
                for (int i = 0; i < rad; i++)
                {
                    currentPosR += rightVector * HScale;
                    if (blacklist.Contains(currentPosR))
                        break;
                    if (Vector3.Distance(center, currentPosR) <= rad * HScale)
                    {
                        var er = FabricGameObject.InstantiateElement<Point>(currentPosR, this, buildVector);
                        subElements.Add(er);

                        //if (Vector3.Distance(center, currentPosR) == rad * HScale)
                        //{
                        //    newWays.Add((er, er.transform.position + rightVector * HScale, rightVector));
                        //}
                    }

                }

                for (int i = 0; i < rad; i++)
                {
                    currentPosL += leftVector * HScale;
                    if (blacklist.Contains(currentPosL))
                        break;

                    if (Vector3.Distance(center, currentPosL) <= rad * HScale)
                    {
                        var el = FabricGameObject.InstantiateElement<Point>(currentPosL, this, buildVector);
                        subElements.Add(el);

                        //if (Vector3.Distance(center, currentPosL) == rad * HScale)
                        //{
                        //    newWays.Add((el, el.transform.position + leftVector * HScale, leftVector));
                        //}
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
                var aboutEl = e.transform.position.About(HScale);
                var aboutCons = from t in subElements
                                where aboutEl.Contains(t.transform.position)
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
            GenNewWays();
            //if (interestPlace != null)
            //    GenRoomEntry();
            //else Debug.Log("interestPlace == Vector3.zero");
        }
    }

}
