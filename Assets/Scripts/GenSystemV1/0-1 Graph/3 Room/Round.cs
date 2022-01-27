using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public class Round : Room
    {
        public override void Generate()
        {
            var rad = (parentElement as Zone).roomSize;
            Vector3 center = transform.position + buildVector * rad * scale;
            Vector3 currentPos = transform.position;
            var rightVector = buildVector.ToRight();
            var leftVector = buildVector.ToLeft();
            var area = parentElement.parentElement as Area;

            for (int j = 0; j < rad * 2 + 1; j++)
            {
                if (blacklist.Contains(currentPos))
                    break;
                subElements.Add(FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector));
                if (currentPos == center)
                    area.ChestPlaces.Add(center);
                if (j == rad * 2)
                    newWays.Insert(0, (subElements.Last(), subElements.Last().transform.position + buildVector * scale, buildVector));

                Vector3 currentPosL, currentPosR;
                currentPosL = currentPosR = currentPos;
                for (int i = 0; i < rad; i++)
                {
                    currentPosR += rightVector * scale;
                    if (blacklist.Contains(currentPosR))
                        break;
                    if (Vector3.Distance(center, currentPosR) <= rad * scale)
                    {
                        var er = FabricGameObject.InstantiateElement<Point>(currentPosR, this, buildVector);
                        subElements.Add(er);

                        if (Vector3.Distance(center, currentPosR) == rad * scale)
                        {
                            newWays.Add((er, er.transform.position + rightVector * scale, rightVector));
                        }
                    }

                }

                for (int i = 0; i < rad; i++)
                {
                    currentPosL += leftVector * scale;
                    if (blacklist.Contains(currentPosL))
                        break;

                    if (Vector3.Distance(center, currentPosL) <= rad * scale)
                    {
                        var el = FabricGameObject.InstantiateElement<Point>(currentPosL, this, buildVector);
                        subElements.Add(el);

                        if (Vector3.Distance(center, currentPosL) == rad * scale)
                        {
                            newWays.Add((el, el.transform.position + leftVector * scale, leftVector));
                        }
                    }
                }
                currentPos += buildVector * scale;
            }
            rootElement = subElements[0];
            if (backElement != null)
            {
                backElement.Connect(rootElement);
            }

            for (int i = 0; i < subElements.Count; i++)
            {
                GraphElement e = subElements[i];
                var aboutEl = e.transform.position.About(scale);
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
        }
    }

}
