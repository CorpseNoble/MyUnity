using UnityEngine;

public abstract class Subzone : GraphElement
{
    public int waySize = SettingGraph.SettingGraphRef.waySize.GetValue();
    public int roomSize = SettingGraph.SettingGraphRef.roomSize.GetValue();
    public int roomLenght = SettingGraph.SettingGraphRef.pathLenght.GetValue();

    protected GraphElement SelectionRoom(Vector3 currentVector, Vector3 currentPos)
    {
        GraphElement currElem;
        if (SettingGraph.SettingGraphRef.roomPercent.GetValue())
            if (SettingGraph.SettingGraphRef.roudRoomPercent.GetValue())
                currElem = FabricGameObject.InstantiateElement<Round>(currentPos, this, currentVector);
            else
                currElem = FabricGameObject.InstantiateElement<Squar>(currentPos, this, currentVector);
        else
            currElem = FabricGameObject.InstantiateElement<Way>(currentPos, this, currentVector);

        return currElem;
    }

}

