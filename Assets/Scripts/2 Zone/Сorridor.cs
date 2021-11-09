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
            if (!blacklist.WayCheckClear(currentPos, currentVector, roomLenght))
                if (SettingGraph.SettingGraphRef.LRPercent.GetValue())
                {
                    if (blacklist.WayCheckClear(currentPos, currentVector.ToLeft(), roomLenght))
                        currentVector = currentVector.ToLeft();
                    else if (blacklist.WayCheckClear(currentPos, currentVector.ToRight(), roomLenght))
                        currentVector = currentVector.ToRight();
                    else
                        break;
                }
                else
                {
                    if (blacklist.WayCheckClear(currentPos, currentVector.ToRight(), roomLenght))
                        currentVector = currentVector.ToRight();
                    else if (blacklist.WayCheckClear(currentPos, currentVector.ToLeft(), roomLenght))
                        currentVector = currentVector.ToLeft();
                    else
                        break;
                }
            GraphElement currElem = SelectionRoom(currentVector, currentPos, i == zoneSize - 1);
            currElem.backElement = preElement;
            currElem.Generate();
            subElements.Add(currElem);
            try
            {
                (preElement, currentPos, currentVector) = currElem.newWays.First();

                currElem.newWays.RemoveAt(0);
                newWays.AddRange(currElem.newWays);
            }
            catch
            {
                Debug.LogWarning(currElem.name + "  : count :" + currElem.newWays.Count());
                break;
            }
        }
        rootElement = subElements.First();

    }

}

