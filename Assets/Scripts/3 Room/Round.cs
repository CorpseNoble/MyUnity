using System.Linq;
using UnityEngine;

public class Round : Room
{
    public override void Generate()
    {
        var rad = (parentElement as Subzone).roomSize;
        Vector3 center = transform.position + buildVector * rad;
        Vector3 currentPos = transform.position;
        var rightVector = buildVector.ToRight();
        var leftVector = buildVector.ToLeft();
        for (int j = 0; j < rad * 2 + 1; j++)
        {
            subElements.Add(FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector));

            if (j == rad * 2)
                newWays.Insert(0, (subElements.Last(), subElements.Last().transform.position + buildVector, buildVector));

            Vector3 currentPosL, currentPosR;
            currentPosL = currentPosR = currentPos;
            for (int i = 0; i < rad; i++)
            {
                

                currentPosL += leftVector;
                currentPosR += rightVector;

                if (Vector3.Distance(center, currentPosL) <= rad)
                {
                    var el = FabricGameObject.InstantiateElement<Point>(currentPosL, this, buildVector);
                    var er = FabricGameObject.InstantiateElement<Point>(currentPosR, this, buildVector);
                    subElements.Add(el);
                    subElements.Add(er);

                    if (Vector3.Distance(center, currentPosL) == rad)
                    {
                        newWays.Add((el, el.transform.position + leftVector, leftVector));
                        newWays.Add((er, er.transform.position + rightVector, rightVector));
                    }
                }
            }
            currentPos += buildVector;
        }
        rootElement = subElements[0];
        backElement.Connect(rootElement);

        foreach (var e in subElements)
        {
            var d = e.transform.position.About();
            var aboutCons = from t in subElements
                            where d.Contains(t.transform.position)
                            select t;


            foreach (var a in aboutCons)
                e.Connect(a);

            e.Generate();
        }
    }
}

