using System.Linq;
using UnityEngine;

public class Squar : Room
{
    public override void Generate()
    {
        Vector3 currentPos = transform.position;
        var rightVector = buildVector.ToRight();
        var leftVector = buildVector.ToLeft();
        for (int j = 0; j < (parentElement as Zone).roomLenght; j++)
        {
            if (blacklist.Contains(currentPos))
                break;
            var se = FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector);

            subElements.Add(se);

            if (j == (parentElement as Zone).roomLenght - 1)
                newWays.Add((subElements.Last(), subElements.Last().transform.position + buildVector, buildVector));

            Vector3 currentPosL, currentPosR;
            currentPosL = currentPosR = currentPos;
            for (int i = 0; i < (parentElement as Zone).roomSize; i++)
            {
                currentPosR += rightVector;
                if (blacklist.Contains(currentPosR))
                    break;
                var pr = FabricGameObject.InstantiateElement<Point>(currentPosR, this, buildVector);
                subElements.Add(pr);
                if (j == (parentElement as Zone).roomLenght - 1 && i == (parentElement as Zone).roomSize - 1)
                {
                    newWays.Add((pr, pr.transform.position + rightVector, rightVector));
                    newWays.Add((pr, pr.transform.position + buildVector, buildVector));
                }
                if (j == (parentElement as Zone).roomLenght - 1 && i == 0)
                {
                    newWays.Add((pr, pr.transform.position + rightVector, rightVector));
                    newWays.Add((pr, pr.transform.position + rightVector.ToRight(), rightVector.ToRight()));
                }
            }

            for (int i = 0; i < (parentElement as Zone).roomSize; i++)
            {
                currentPosL += leftVector;
                if (blacklist.Contains(currentPosL))
                    break;
                var pl = FabricGameObject.InstantiateElement<Point>(currentPosL, this, buildVector);
                subElements.Add(pl);
                if (j == (parentElement as Zone).roomLenght - 1 && i == (parentElement as Zone).roomSize - 1)
                {
                    newWays.Add((pl, pl.transform.position + leftVector, leftVector));
                    newWays.Add((pl, pl.transform.position + buildVector, buildVector));
                }
                if (j == (parentElement as Zone).roomLenght - 1 && i == 0)
                {
                    newWays.Add((pl, pl.transform.position + leftVector, leftVector));
                    newWays.Add((pl, pl.transform.position + leftVector.ToLeft(), leftVector.ToLeft()));
                }

            }

            currentPos += buildVector;

        }

        rootElement = subElements[0];
        if (backElement != null)
        {
            backElement.Connect(rootElement);
        }

        for (int i = 0; i < subElements.Count; i++)
        {
            GraphElement e = subElements[i];
            var d = e.transform.position.About();
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

