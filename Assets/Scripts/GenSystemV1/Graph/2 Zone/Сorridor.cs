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
            GraphElement preElement = backElementLow;

            for (int i = 0; i < zoneSize; i++)
            {
                if (!blacklist.FullLRCheck(currentPos, currentVector*HScale, roomSize, ref currentVector))
                    break;
               
                GraphElement currElem = SelectionRoom(currentVector, currentPos, i >= zoneSize - 1);
                currElem.backElement = preElement;
                currElem.Generate();
                subElements.Add(currElem);
                if (currElem.newWays.Count > 0)
                {
                    var newWay = PrefsGraph.Instant.SettingGraph.SelectRandElement(currElem.newWays);
                    (preElement, currentPos, currentVector) = newWay;

                    currElem.newWays.Remove(newWay);
                    newWays.AddRange(currElem.newWays);
                }
                else
                {
                    Debug.Log("currElem.newWays.Count > 0");
                }
               
            }
            rootElement = subElements.First();

            if (backElement != null )
                number = backElement.number + 1;
            gameObject.name += " " + number;
        }

    }
}

