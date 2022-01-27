using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GenSystemV1
{
    public abstract class Room : GraphElement
    {
        [Header("Room")]
        public Point interestPlace;
        public int countSpawners = 1;
        public bool saveZone = false;

        public static bool first = false;
        protected int roomSize;
        public override void Generate()
        {
            roomSize = (parentElement as Zone).roomSize;
            if (!first)
            {
                first = true;
                saveZone = true;
            }
        }
        public void GenNewWays()
        {
            var walls = new List<Vector3>()
            {
                Vector3.forward,
                Vector3.right,
                Vector3.back,
                Vector3.left
            };
            var wallPoints = new List<GraphElement>(subElements.Where(s => s.connectElements.Count < 4).Select(c => c));
            foreach (var wp in wallPoints)
            {
                var pos = wp.transform.position;
                foreach (var w in walls)
                {
                    var newPos = pos + w * HScale;
                    var has = !newWays.Where(c => c.position == newPos).Any();
                    var black = !blacklist.Contains(newPos);
                    if (has)
                        if (black)
                            newWays.Add(new NewWay(wp, newPos, w));
                }
            }
        }
        public void GenRoomEntry()
        {
            if (interestPlace != null)
                if (!saveZone)
                {
                    FabricGameObject.InstantiatePrefabPoint(PrefsGraph.Instant.elementsData.Chest, interestPlace);
                    countSpawners = subElements.Count / roomSize;
                    if (PrefsGraph.Instant.SettingGraph.EnemyPercent.GetValue())
                    {
                        var spawner = PrefsGraph.Instant.elementsData.MonsterSpawners.First();
                        var elements = subElements.Cast<Point>().Where(c => c.connectElements.Count > 3 && !c.pleced).Select(c => c);
                        var points = PrefsGraph.Instant.SettingGraph.SelectRandElements(countSpawners, elements);
                        foreach (var p in points)
                        {
                            FabricGameObject.InstantiatePrefabPoint(spawner, p);
                        }

                    }
                    //if (PrefsGraph.Instant.SettingGraph.TrapPercent.GetValue())
                    //{

                    //}
                }
                else
                {
                    FabricGameObject.InstantiatePrefabPoint(PrefsGraph.Instant.elementsData.Bonfire, interestPlace);
                }
        }
    }

}
