using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GenSystemV1
{
    public class Point : GraphElement
    {
        [Header("Point")]
        public List<GameObject> geometric = new List<GameObject>();
        public bool pleced = false;

        Area _area;
        bool _save = false;
        //0 - floor
        //1-4 - walls

        public override void Generate()
        {
            _area = parentElement.parentElement.parentElement as Area;
            _save = (parentElement as Room).saveZone;
            var ground = FabricGameObject.InstGro(transform.position, transform);
            if (_save)
                SavePoint(ground);
            geometric.Add(ground);


            var roof = FabricGameObject.InstRoof(transform.position.StepV(hight - 1), transform);
            _area.lightPlaces.Add(roof.GetComponent<WallLightScript>().LightPlace);
            geometric.Add(roof);

            blacklist.Add(this);
        }

        private void InstConType(ConnectType connectType, Vector3 pos, Vector3 vector)
        {
            switch (connectType)
            {
                case ConnectType.Path: break;
                case ConnectType.Door:
                    geometric.AddRange(FabricGameObject.InstDoorH(pos, transform, vector, hight));
                    break;
                case ConnectType.BrokenWall:
                    geometric.AddRange(FabricGameObject.InstLatticeH(pos, transform, vector, hight));
                    break;
                case ConnectType.Lattice:
                    geometric.AddRange(FabricGameObject.InstLatticeH(pos, transform, vector, hight));
                    break;
            }
        }

        public void GenWalls()
        {
            var pullars = new[]
            {
            (Vector3.forward + Vector3.right)*0.5f,
            (Vector3.forward + Vector3.left)*0.5f,
            (Vector3.back + Vector3.right)*0.5f,
            (Vector3.back + Vector3.left)*0.5f,
            };
            var walls = new List<Vector3>()
            {
                Vector3.forward,
                Vector3.right,
                Vector3.back,
                Vector3.left
            };
            //var hasWall = false;
            foreach (var ce in connectElements)
            {
                //if (ce.GetConnect(this).connectElements.Count < 4)
                //    hasWall = true;

                var way = (ce.GetConnect(this).transform.position - transform.position).normalized;
                var currP = transform.position.StepH(way, 0.5f);
                if (!ce.instanted)
                {
                    ce.instanted = true;
                    var ground = FabricGameObject.InstPath(currP, transform, way);
                    if (_save)
                        SavePoint(ground);
                    geometric.Add(ground);
                    InstConType(ce.connectType, currP, way);
                }

                walls.Remove(way);
            }
            foreach (var w in walls)
            {
                var currP = transform.position.StepH(w, 0.5f);
                if (!blacklist.ContainsWall(currP))
                {
                    blacklist.AddWall(currP);
                    var ground = FabricGameObject.InstPath(currP, transform, w);
                    if (_save)
                        SavePoint(ground);
                    geometric.Add(ground);
                }

                geometric.AddRange(FabricGameObject.InstWallH(currP, transform, w, hight));

            }

            foreach (var p in pullars)
            {
                var currP = transform.position.StepH(p);
                if (!blacklist.ContainsPillarGround(currP))
                {
                    blacklist.AddPillarGround(currP);
                    var ground = FabricGameObject.InstGrPillar(currP, transform);
                    if (_save)
                        SavePoint(ground);
                    geometric.Add(ground);

                }
                if (!blacklist.ContainsPillar(currP))
                {
                    if (connectElements.Where(c => c.connectType != ConnectType.Path).Any() || walls.Count > 0 || !PrefsGraph.Instant.SettingGraph.pillarAboutWall)
                    {
                        blacklist.AddPillar(currP);
                        geometric.AddRange(FabricGameObject.InstHiPillar(currP, transform, hight));

                    }
                }
            }
        }

        private static void SavePoint(GameObject ground)
        {
            ground.layer = 6;
            if (ground.transform.childCount > 0)
            {
                for (int i = 0; i < ground.transform.childCount; i++)
                {
                    var child = ground.transform.GetChild(i);
                    child.gameObject.layer = 6;
                }
            }
        }
    }

}
