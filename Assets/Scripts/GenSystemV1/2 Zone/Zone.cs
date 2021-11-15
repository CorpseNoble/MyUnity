using UnityEngine;

public abstract class Zone : GraphElement
{
    public int waySize = SettingGraph.SettingGraphRef.waySize.GetValue();
    public int roomSize = SettingGraph.SettingGraphRef.roomSize.GetValue();
    public int roomLenght = SettingGraph.SettingGraphRef.pathLenght.GetValue();

    /// <summary>
    /// Select and setup room on currentPos
    /// </summary>
    /// <param name="currentVector"></param>
    /// <param name="currentPos"></param>
    /// <returns></returns>
    protected GraphElement SelectionRoom(Vector3 currentVector, Vector3 currentPos, bool endEl = false)
    {
        GraphElement currElem;
        if (SettingGraph.SettingGraphRef.roomPercent.GetValue() || endEl)
            if (SettingGraph.SettingGraphRef.roudRoomPercent.GetValue())
                currElem = FabricGameObject.InstantiateElement<Round>(currentPos, this, currentVector);
            else
                currElem = FabricGameObject.InstantiateElement<Squar>(currentPos, this, currentVector);
        else
            currElem = FabricGameObject.InstantiateElement<Way>(currentPos, this, currentVector);

        return currElem;
    }

}

