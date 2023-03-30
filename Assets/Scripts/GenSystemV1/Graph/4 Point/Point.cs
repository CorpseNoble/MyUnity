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
        public bool stairs = false;

        Area _area;
        bool _save = false;
        //0 - floor
        //1-4 - walls

        public override void Generate()
        {
            _area = parentElement.parentElement.parentElement as Area;
            _save = (parentElement as Room).saveZone;

            var ground =
                !stairs ?
                FabricGameObject.InstGro(transform.position, transform) :
                FabricGameObject.InstStGro(transform.position, transform, buildVector);
            if (_save)
                SavePoint(ground);
            geometric.Add(ground);


            var roof =
                 !stairs ?
                FabricGameObject.InstRoof(transform.position.StepV(hight - 1), transform) :
                FabricGameObject.InstStRoof(transform.position.StepV(hight - 1), transform, buildVector);
            //_area.lightPlaces.Add(roof.GetComponent<WallLightScript>().LightPlace);
            geometric.Add(roof);

            blacklist.Add(this);
            for (int i = 1; i < hight; i++)
            {
                blacklist.Add(FabricGameObject.InstantiateElement<Point>(transform.position.StepV(i), this, buildVector));
                if (i == hight - 1 && stairs)
                    blacklist.Add(FabricGameObject.InstantiateElement<Point>(transform.position.StepV(i + 1), this, buildVector));
            }

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
                    geometric.AddRange(FabricGameObject.InstWallWindowH(pos, transform, vector, hight));
                    break;
                case ConnectType.Window:
                    geometric.AddRange(FabricGameObject.InstWallWindowH(pos, transform, vector, hight));
                    break;
            }
        }

        public void GenWalls()
        {

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

                var way = (ce.GetConnect(this).transform.position - transform.position);
                way.y = 0;
                way.Normalize();
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
                if (!blacklist.ContainsWallGr(currP))
                {
                    GameObject ground;
                    if (stairs)
                    {
                        if(buildVector.ToRight() == w)
                        {
                            ground = FabricGameObject.InstStRPath(currP, transform, w);
                        }
                        else if (buildVector.ToLeft() == w)
                        {
                            ground = FabricGameObject.InstStLPath(currP, transform, w);
                        }
                        else
                        {
                            ground = FabricGameObject.InstPath(currP, transform, w);
                            blacklist.AddWallGr(currP);

                        }
                    }
                    else
                    {
                        ground = FabricGameObject.InstPath(currP, transform, w);
                        blacklist.AddWallGr(currP);

                    }

                    if (_save)
                        SavePoint(ground);
                    geometric.Add(ground);
                }
                if (!stairs)
                {
                    geometric.AddRange(FabricGameObject.InstWallH(currP, transform, w, hight));
                }
                else
                {
                    if (buildVector.ToRight() == w)
                        geometric.AddRange(FabricGameObject.InstStWallRH(currP, transform, w, hight));
                    else if (buildVector.ToLeft() == w)
                        geometric.AddRange(FabricGameObject.InstStWallLH(currP, transform, w, hight));
                    else
                        geometric.AddRange(FabricGameObject.InstWallH(currP, transform, w, hight));

                }

            }
            var pillars = new[]
            {
            (buildVector + buildVector.ToLeft())*0.5f,
            (buildVector + buildVector.ToRight())*0.5f,
            (buildVector.Abort() + buildVector.ToLeft())*0.5f,
            (buildVector.Abort() + buildVector.ToRight())*0.5f,
            };
            for (int i = 0; i < pillars.Length; i++)
            {
                Vector3 p = pillars[i];
                var currP = transform.position.StepH(p);
                bool b = true;
                if (stairs && i > 1)
                {
                    currP = currP.StepV(1);

                }
                if (!blacklist.ContainsPillarGround(currP) && b)
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
