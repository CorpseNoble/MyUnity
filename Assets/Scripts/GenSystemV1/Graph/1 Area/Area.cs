using System.Linq;
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

        [Range(0.1f, 10f)] public float llightDistMulti = 1.5f;

        public const float Light_Range = 50f;

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
            if (!blacklist.FullLRCheck(currentPos, currentVector, zoneSize, ref currentVector))
                return;

            countSubzone--;
            GraphElement currElem;
            currElem = FabricGameObject.InstantiateElement<Сorridor>(currentPos, this, currentVector);

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

        public void GenLight()
        {
            while (lightPlaces.Count > 0)
            {
                GameObject lp = lightPlaces[0];
                FabricGameObject.InstLight(lp.transform.position, lp.transform, lp.transform.forward);
                lightPlaces.Remove(lp);
                for (int j = 0; j < lightPlaces.Count; j++)
                {
                    GameObject lp2 = lightPlaces[j];
                    if (Vector3.Distance(lp.transform.position, lp2.transform.position) <=  llightDistMulti * Light_Range / (hight * FabricGameObject.elementsData.HorScale))
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

