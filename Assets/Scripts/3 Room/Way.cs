using System.Linq;
using UnityEngine;


public class Way : Room
{
    public override void Generate()
    {
        Vector3 currentPos = transform.position;
        var pathLenght = SettingGraph.SettingGraphRef.pathLenght.GetValue();
     
        for (int j = 0; j < pathLenght; j++)
        {
            var se = FabricGameObject.InstantiateElement<Point>(currentPos, this, buildVector);

            if (j > 0)
            {
                subElements[j - 1].Connect(se);
            }
            subElements.Add(se);

            if (SettingGraph.SettingGraphRef.noizePercent.GetValue())
                if (SettingGraph.SettingGraphRef.LRPercent.GetValue())
                    buildVector = buildVector.ToLeft();
                else 
                    buildVector = buildVector.ToRight();

            currentPos += buildVector;

        }
        newWays.Add((subElements.Last(),subElements.Last().transform.position + buildVector, buildVector));
        rootElement = subElements.First();
        if(backElement != null)
        {
            backElement.Connect(rootElement);
        }
        foreach (var e in subElements)
        {
            
            e.Generate();
        }

        
    }
}

