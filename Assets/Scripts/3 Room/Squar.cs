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
            var se = FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector);

            subElements.Add(se);

            if (j == (parentElement as Zone).roomLenght - 1)
                newWays.Add((subElements.Last(),subElements.Last().transform.position + buildVector, buildVector));

            Vector3 currentPosL, currentPosR;
            currentPosL = currentPosR = currentPos;
            for (int i = 0; i < (parentElement as Zone).roomSize; i++)
            { 
                currentPosL += leftVector;
                currentPosR += rightVector;

                var pl = FabricGameObject.InstantiateElement<Point>(currentPosL, this, buildVector) ;
                var pr = FabricGameObject.InstantiateElement<Point>(currentPosR, this, buildVector) ;

                subElements.Add(pl);
                subElements.Add(pr);

                if (j == (parentElement as Zone).roomLenght - 1 && i == (parentElement as Zone).roomSize - 1)
                {
                    newWays.Add((pl,pl.transform.position + leftVector, leftVector));
                    newWays.Add((pr,pr.transform.position + rightVector, rightVector));
                }
            }

            currentPos += buildVector;

        }

        rootElement = subElements[0];
        if (backElement != null)
        {
            backElement.Connect(rootElement);
        }

        foreach (var e in subElements)
        {
           var d = e.transform.position.About();
            var aboutCons = from t in subElements
                    where d.Contains(t.transform.position)
                    select t;


            foreach(var a in aboutCons)
                e.Connect(a);

            e.Generate();
        }
    }
}

