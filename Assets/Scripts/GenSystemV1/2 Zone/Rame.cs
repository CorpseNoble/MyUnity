using UnityEngine;

public class Rame : Zone
{
    int countRoom = SettingGraph.SettingGraphRef.subzoneSize.GetValue() * 2;
    public override void Generate()
    {
        Vector3 currentPos = transform.position;
        Vector3 currentVector = buildVector;

        var currElem = SelectionRoom(currentVector, currentPos);
        ElementSetUp(currentVector, currentPos, currElem,backElement);
    }

    protected void ElementSetUp(Vector3 currentVector, Vector3 currentPos, GraphElement currElem, GraphElement preElem)
    {
        countRoom--;
        currElem.buildVector = currentVector;
        currElem.backElement = preElem;
        currElem.Generate();
        subElements.Add(currElem);
        for (int i = 0; i < currElem.newWays.Count; i++)
        {
            if (countRoom > 0 && SettingGraph.SettingGraphRef.roomPercent.GetValue())
            {
                var (elem,pos, forw) = currElem.newWays[i];
                currElem.newWays.RemoveAt(i);
                i--;
                currentVector = forw;
                currentPos = pos;
                var currElem1 = SelectionRoom(currentVector, currentPos);
                ElementSetUp(currentVector, currentPos, currElem1,elem);
            }
        }
        newWays.AddRange(currElem.newWays);

    }
}

