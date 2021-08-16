using System.Linq;
using UnityEngine;

public class Round : Room
{
    public override void Generate()
    {
        var rad = (parentElement as Zone).roomSize;
        Vector3 center = transform.position + buildVector * rad;
        Vector3 currentPos = transform.position;
        var rightVector = buildVector.ToRight();
        var leftVector = buildVector.ToLeft();
        for (int j = 0; j < rad * 2 + 1; j++)
        {
            if (blacklist.Contains(currentPos))
                break;
            subElements.Add(FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector));

            if (j == rad * 2)
                newWays.Insert(0, (subElements.Last(), subElements.Last().transform.position + buildVector, buildVector));

            Vector3 currentPosL, currentPosR;
            currentPosL = currentPosR = currentPos;
            for (int i = 0; i < rad; i++)
            {
                currentPosR += rightVector;
                if (blacklist.Contains(currentPosR))
                    break;
                if (Vector3.Distance(center, currentPosR) <= rad)
                {
                    var er = FabricGameObject.InstantiateElement<Point>(currentPosR, this, buildVector);
                    subElements.Add(er);

                    if (Vector3.Distance(center, currentPosR) == rad)
                    {
                        newWays.Add((er, er.transform.position + rightVector, rightVector));
                    }
                }

            }

            for (int i = 0; i < rad; i++)
            {
                currentPosL += leftVector;
                if (blacklist.Contains(currentPosL))
                    break;

                if (Vector3.Distance(center, currentPosL) <= rad)
                {
                    var el = FabricGameObject.InstantiateElement<Point>(currentPosL, this, buildVector);
                    subElements.Add(el);

                    if (Vector3.Distance(center, currentPosL) == rad)
                    {
                        newWays.Add((el, el.transform.position + leftVector, leftVector));
                    }
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
            var aboutEl = e.transform.position.About();
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

