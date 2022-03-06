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
        //генерация точек для создания новых GraphElement
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
                    var newPos = pos.StepH(w);
                    var has = !newWays.Where(c => c.position == newPos).Any();
                    var black = !blacklist.Contains(newPos);
                    if (has)
                        if (black)
                            newWays.Add(new NewWay(wp, newPos, w));
                }
            }
        }

        protected void GenPointEntry(bool sample = false)
        {
            if (backElement != null)
            {
                var con = backElement.Connect(rootElement);

                if (backElement.parentElement.parentElement != parentElement)
                    con.connectType = ConnectType.Door;

                var room = backElement.parentElement as Room;
                if (room.saveZone)
                    con.connectType = ConnectType.Door;
            }
            if (!sample)
                for (int i = 0; i < subElements.Count; i++)
                {
                    GraphElement e = subElements[i];
                    var aboutEl = e.transform.position.About();
                    var aboutCons = from t in subElements
                                    where aboutEl.Contains(t.transform.position)
                                    select t;

                    if (aboutCons.Count() == 0)
                    {
                        subElements.RemoveAt(i);
                        i--;
                        continue;
                    }
                    foreach (var a in aboutCons)
                        e.Connect(a);

                    //Lattice gen
                    foreach (var elem in aboutEl)
                    {
                        if (aboutCons.Where(c => c.transform.position == elem).Any())
                            continue;
                        if (blacklist.Contains(elem))
                        {
                            var point = blacklist.SelectPoint(elem);
                            e.Connect(point, ConnectType.Lattice);
                        }
                    }
                }

            foreach (var e in subElements)
            {
                e.Generate();
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
                        var elements = subElements.Cast<Point>()
                            .Where(c => c.connectElements
                                .Where(w => w.connectType == ConnectType.Path)
                                .Select(k => k).Count() > 3 && !c.pleced)
                            .Select(c => c);
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
