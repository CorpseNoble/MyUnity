using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public class Area : GraphElement
    {
        public int countSubzone;
        public PointElementsData elementsData;
        public List<GameObject> Doors = new List<GameObject>();
        public List<GameObject> LightPlaces = new List<GameObject>();
        public List<Vector3> ChestPlaces = new List<Vector3>();
        public List<GameObject> Chest = new List<GameObject>();

        [Range(1, 5)] public float llightDistMulti = 1.5f;
        public override void Generate()
        {
            Vector3 currentPos = transform.position;
            Vector3 currentVector = buildVector;
            countSubzone = PrefsGraph.Instant.SettingGraph.maxZoneCount.GetValue();
            SelectSubZone(currentPos, currentVector, null);
            rootElement = subElements.First();
        }
        public void SelectSubZone(Vector3 currentPos, Vector3 currentVector, GraphElement preElement)
        {
            int foo = 10;
            if (!blacklist.WayCheckClear(currentPos, currentVector, foo))
                if (PrefsGraph.Instant.SettingGraph.LRPercent.GetValue())
                {
                    if (blacklist.WayCheckClear(currentPos, currentVector.ToLeft(), foo))
                        currentVector = currentVector.ToLeft();
                    else if (blacklist.WayCheckClear(currentPos, currentVector.ToRight(), foo))
                        currentVector = currentVector.ToRight();
                    else
                        return;
                }
                else
                {
                    if (blacklist.WayCheckClear(currentPos, currentVector.ToRight(), foo))
                        currentVector = currentVector.ToRight();
                    else if (blacklist.WayCheckClear(currentPos, currentVector.ToLeft(), foo))
                        currentVector = currentVector.ToLeft();
                    else
                        return;
                }
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

            var countnewZone = PrefsGraph.Instant.SettingGraph.nextZoneCount.GetValue();
            countnewZone = countnewZone < 1 ? 1 : countnewZone;
            if (countSubzone < countnewZone)
                countnewZone = countSubzone;

            var nextZones = PrefsGraph.Instant.SettingGraph.SelectRandElements(countnewZone, currElem.newWays).ToList();
            for (int i = 0; i < nextZones.Count; i++)
            {
                SelectSubZone(nextZones[i].pos, nextZones[i].forw, nextZones[i].elem);
            }
        }

        public void GenDoor()
        {
            var cons = new List<Connect>();
            foreach (var zone in subElements)
            {
                cons = cons.Union(zone.connectElements).ToList();
            }
            foreach (var con in cons)
            {
                var connect = con.lowConnect?.lowConnect;
                var vector = (connect.Elements[1].transform.position - connect.Elements[0].transform.position).normalized;
                var pos = connect.Elements[0].transform.position + vector * scale * 0.5f;

                Doors.Add(FabricGameObject.InstantiateVectoredPrefab(elementsData.Door, pos, transform, vector));
            }
        }

        public void GenLight()
        {
            while (LightPlaces.Count > 0)
            {
                GameObject lp = LightPlaces[0];
                FabricGameObject.InstantiateVectoredPrefab(elementsData.Light, lp.transform.position, lp.transform, -lp.transform.forward);
                LightPlaces.Remove(lp);
                for (int j = 0; j < LightPlaces.Count; j++)
                {
                    GameObject lp2 = LightPlaces[j];
                    if (Vector3.Distance(lp.transform.position, lp2.transform.position) <= scale * llightDistMulti)
                    {
                        LightPlaces.Remove(lp2);
                        j--;
                    }
                }
            }
        }

        public void GenChest()
        {
            foreach (var cp in ChestPlaces)
            {
                Chest.Add(FabricGameObject.InstantiatePrefab(elementsData.Chest, cp, transform));
            }
        }
    }
}

