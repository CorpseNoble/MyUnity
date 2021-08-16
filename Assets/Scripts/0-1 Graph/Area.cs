using System.Linq;
using UnityEngine;

public class Area : GraphElement
{
    public TexturePack texturePack;
    public int countSubzone;

    public override void Generate()
    {
        Vector3 currentPos = transform.position;
        Vector3 currentVector = buildVector;
        countSubzone = SettingGraph.SettingGraphRef.maxZoneCount.GetValue();
        SelectSubZone(currentPos, currentVector, null);
        rootElement = subElements.First();
    }
    public void SelectSubZone(Vector3 currentPos, Vector3 currentVector, GraphElement preElement)
    {
        countSubzone--;
        GraphElement currElem;
        currElem = FabricGameObject.InstantiateElement<Сorridor>(currentPos, this, currentVector);

        //if (SettingGraph.SettingGraphRef.corridorPercent.GetValue())
        //else
        //    currElem = FabricGameObject.InstantiateElement<Rame>(currentPos, this, currentVector);

        currElem.parentElement = this;
        currElem.backElement = preElement;
        currElem.buildVector = currentVector;
        currElem.Generate();
        subElements.Add(currElem);

        var countnewZone = SettingGraph.SettingGraphRef.nextZoneCount.GetValue();
        countnewZone = countnewZone < 1 ? 1 : countnewZone;
        if (countSubzone < countnewZone)
            countnewZone = countSubzone;

        var nextZones = SettingGraph.SettingGraphRef.SelectRandElements(countnewZone, currElem.newWays).ToList();
        for (int i = 0; i < nextZones.Count; i++)
        {
            SelectSubZone(nextZones[i].pos, nextZones[i].forw, nextZones[i].elem);
        }
    }
}

