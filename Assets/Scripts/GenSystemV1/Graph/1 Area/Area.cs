﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public class Area : GraphElement
    {
        [Header("Area")]
        public int countSubzone = 10;
        public PointElementsData elementsData;
        public List<GameObject> lightPlaces = new List<GameObject>();

        [Range(1, 10)] public float llightDistMulti = 1.5f;
        public override void Generate()
        {
            Room.first = false;
            lightPlaces.Clear();
            hight = PrefsGraph.Instant.SettingGraph.hight;
            Vector3 currentPos = transform.position;
            Vector3 currentVector = buildVector;
            countSubzone = PrefsGraph.Instant.SettingGraph.maxZoneCount;
            SelectSubZone(currentPos, currentVector, null);
            rootElement = subElements.First();
        }
        public void SelectSubZone(Vector3 currentPos, Vector3 currentVector, GraphElement preElement)
        {
            int zoneSize = ((int)(PrefsGraph.Instant.SettingGraph.subzoneSize.avarage * PrefsGraph.Instant.SettingGraph.roomSize.avarage));
            if (!blacklist.FullLRCheck(currentPos, currentVector * HScale, zoneSize, ref currentVector))
                return;
           
            countSubzone--;
            GraphElement currElem;
            currElem = FabricGameObject.InstantiateElement<Сorridor>(currentPos, this, currentVector);

            //if (SettingGraph.SettingGraphRef.corridorPercent.GetValue())
            //else
            //    currElem = FabricGameObject.InstantiateElement<Rame>(currentPos, this, currentVector);

            currElem.parentElement = this;
            currElem.backElementLow = preElement;
            currElem.backElement = preElement?.parentElement.parentElement;
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
                SelectSubZone(nextZones[i].position, nextZones[i].vector, nextZones[i].elemement);
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
                var pos = connect.Elements[0].transform.position + vector * HScale * 0.5f;

                FabricGameObject.InstantiateVectoredPrefab(elementsData.Door, pos, connect.Elements[0].transform, vector);
                for(int i = 1; i < hight; i++)
                {
                    FabricGameObject.InstantiateVectoredPrefab(elementsData.Wall, pos + i * VScale * Vector3.up, connect.Elements[0].transform, vector);
                    FabricGameObject.InstantiateVectoredPrefab(elementsData.Wall, pos + i * VScale * Vector3.up, connect.Elements[0].transform, -vector);
                }
            }
        }

        public void GenLight()
        {
            while (lightPlaces.Count > 0)
            {
                GameObject lp = lightPlaces[0];
                FabricGameObject.InstantiateVectoredPrefab(elementsData.Light, lp.transform.position, lp.transform, -lp.transform.forward);
                lightPlaces.Remove(lp);
                for (int j = 0; j < lightPlaces.Count; j++)
                {
                    GameObject lp2 = lightPlaces[j];
                    if (Vector3.Distance(lp.transform.position, lp2.transform.position) <= HScale * llightDistMulti / hight)
                    {
                        lightPlaces.Remove(lp2);
                        j--;
                    }
                }
            }
        }
        public void GenRoomEntry()
        {
            foreach (var s in subElements)
            {
                foreach (var s1 in s.subElements)
                {
                    var s12 = s1 as Room;
                    if (s12.interestPlace != null)
                        s12.GenRoomEntry();
                }
            }
        }

    }
}
