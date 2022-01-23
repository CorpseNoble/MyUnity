using System.Linq;
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
            for (int j = 0; j < roomLenght; j++)
            {
                if (blacklist.Contains(currentPos))
                    break;
                var se = FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector);

                if (j == roomLenght / 2)
                    area.ChestPlaces.Add(currentPos);

                subElements.Add(se);

                if (j == roomLenght - 1)
                    newWays.Add((subElements.Last(), subElements.Last().transform.position + buildVector * scale, buildVector));

                Vector3 currentPosL, currentPosR;
                currentPosL = currentPosR = currentPos;
                for (int i = 0; i < roomSize / 2 - 1; i++)
                {
                    currentPosR += rightVector * scale;
                    if (blacklist.Contains(currentPosR))
                        break;
                    var pr = FabricGameObject.InstantiateElement<Point>(currentPosR, this, buildVector);
                    subElements.Add(pr);
                    if (j == roomLenght - 1 && i == roomSize / 2 - 1)
                    {
                        newWays.Add((pr, pr.transform.position + rightVector * scale, rightVector));
                        newWays.Add((pr, pr.transform.position + buildVector * scale, buildVector));
                    }
                    if (j == roomLenght - 1 && i == 0)
                    {
                        newWays.Add((pr, pr.transform.position + rightVector * scale, rightVector));
                        newWays.Add((pr, pr.transform.position + rightVector.ToRight() * scale, rightVector.ToRight()));
                    }
                }

                for (int i = 0; i < roomSize / 2 - 1; i++)
                {
                    currentPosL += leftVector * scale;
                    if (blacklist.Contains(currentPosL))
                        break;
                    var pl = FabricGameObject.InstantiateElement<Point>(currentPosL, this, buildVector);
                    subElements.Add(pl);
                    if (j == roomLenght - 1 && i == roomSize / 2 - 1)
                    {
                        newWays.Add((pl, pl.transform.position + leftVector * scale, leftVector));
                        newWays.Add((pl, pl.transform.position + buildVector * scale, buildVector));
                    }
                    if (j == roomLenght - 1 && i == 0)
                    {
                        newWays.Add((pl, pl.transform.position + leftVector * scale, leftVector));
                        newWays.Add((pl, pl.transform.position + leftVector.ToLeft() * scale, leftVector.ToLeft()));
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
                var d = e.transform.position.About(scale);
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
        }
    }

}
