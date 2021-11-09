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

            if (blacklist.Contains(currentPos) || SettingGraph.SettingGraphRef.noizePercent.GetValue())
                if (SettingGraph.SettingGraphRef.LRPercent.GetValue())
                    buildVector.ToTheLeft();
                else
                    buildVector.ToTheRight();

            if (SettingGraph.SettingGraphRef.noizePercent.GetValue())
            {
                if (SettingGraph.SettingGraphRef.LRPercent.GetValue())
                {
                    if (!blacklist.Contains(se.transform.position + buildVector.ToLeft()))
                        newWays.Add((se, se.transform.position + buildVector.ToLeft(), buildVector.ToLeft()));
                }
                else
                {
                    if (!blacklist.Contains(se.transform.position + buildVector.ToRight()))
                        newWays.Add((se, se.transform.position + buildVector.ToRight(), buildVector.ToRight()));
                }
            }
            currentPos += buildVector;

            if (blacklist.Contains(currentPos))
                break;

        }
        newWays.Insert(0,(subElements.Last(), subElements.Last().transform.position + buildVector, buildVector));
        newWays.Add((subElements.Last(), subElements.Last().transform.position + buildVector.ToLeft(), buildVector.ToLeft()));
        newWays.Add((subElements.Last(), subElements.Last().transform.position + buildVector.ToRight(), buildVector.ToRight()));
        rootElement = subElements.First();
        if (backElement != null)
        {
            backElement.Connect(rootElement);
        }
        foreach (var e in subElements)
        {

            e.Generate();
        }


    }
}

