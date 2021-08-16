using System.Linq;
using UnityEngine;

public class Сorridor : Zone
{

    public override void Generate()
    {
        Vector3 currentPos = transform.position;
        Vector3 currentVector = buildVector;
        GraphElement preElement = backElement;
        
        var zoneSize = SettingGraph.SettingGraphRef.subzoneSize.GetValue();
        for (int i = 0; i < zoneSize; i++)
        {
            var currElem = SelectionRoom(currentVector, currentPos);
            currElem.backElement = preElement;
            currElem.Generate();
            subElements.Add(currElem);

            (preElement, currentPos, currentVector) = currElem.newWays.First();

            currElem.newWays.RemoveAt(0);
            newWays.AddRange(currElem.newWays);

            if (i == zoneSize - 1)
            {
               

                //if (SettingGraph.SettingGraphRef.noizePercent.GetValue())
                //    if (SettingGraph.SettingGraphRef.LRPercent.GetValue())
                //        currentVector = currentVector.ToLeft();
                //    else
                //        currentVector = currentVector.ToRight();
            }
        }
        rootElement = subElements.First();

    }

}

