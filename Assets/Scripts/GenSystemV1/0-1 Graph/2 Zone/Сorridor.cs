using System.Linq;
using UnityEngine;
namespace Assets.Scripts.GenSystemV1
{
    public class Сorridor : Zone
    {

        public override void Generate()
        {
            base.Generate();
            Vector3 currentPos = transform.position;
            Vector3 currentVector = buildVector;
            GraphElement preElement = backElement;

            var zoneSize = PrefsGraph.Instant.SettingGraph.subzoneSize.GetValue();
            for (int i = 0; i < zoneSize; i++)
            {
                if (!blacklist.WayCheckClear(currentPos, currentVector, roomSize))
                    if (PrefsGraph.Instant.SettingGraph.LRPercent.GetValue())
                    {
                        if (blacklist.WayCheckClear(currentPos, currentVector.ToLeft(), roomSize))
                            currentVector = currentVector.ToLeft();
                        else if (blacklist.WayCheckClear(currentPos, currentVector.ToRight(), roomSize))
                            currentVector = currentVector.ToRight();
                        else
                            break;
                    }
                    else
                    {
                        if (blacklist.WayCheckClear(currentPos, currentVector.ToRight(), roomSize))
                            currentVector = currentVector.ToRight();
                        else if (blacklist.WayCheckClear(currentPos, currentVector.ToLeft(), roomSize))
                            currentVector = currentVector.ToLeft();
                        else
                            break;
                    }
                GraphElement currElem = SelectionRoom(currentVector, currentPos, i >= zoneSize - 1);
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
}

