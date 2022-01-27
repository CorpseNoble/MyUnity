using UnityEngine;
using System.Collections.Generic;

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
            var ground = FabricGameObject.InstantiateGroundPrefab(_area.elementsData.Ground, transform.position, transform);
            if (_save)
                SavePoint(ground);
            geometric.Add(ground);


            var roof = FabricGameObject.InstantiateGroundPrefab(_area.elementsData.Roof, transform.position + (hight-1) * VScale * Vector3.up, transform);
            _area.lightPlaces.Add(roof.GetComponent<WallLightScript>().LightPlace);
            geometric.Add(roof);

            blacklist.Add(this);
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
                var currP = transform.position + way * HScale * 0.5f;
                if (!ce.instanted)
                {
                    ce.instanted = true;
                    var ground = FabricGameObject.InstantiateVectoredPrefab(_area.elementsData.GroundPathWay, currP, transform, way);
                    if (_save)
                        SavePoint(ground);
                    geometric.Add(ground);

                }

                walls.Remove(way);
            }
            foreach (var w in walls)
            {
                var currP = transform.position + w * HScale * 0.5f;
                if (!blacklist.ContainsWall(currP))
                {
                    blacklist.AddWall(currP);
                    var ground = FabricGameObject.InstantiateVectoredPrefab(_area.elementsData.GroundPathWay, currP, transform, w);
                    if (_save)
                        SavePoint(ground);
                    geometric.Add(ground);
                }

                for (int i = 0; i < hight; i++)
                {
                    var wall2 = FabricGameObject.InstantiateVectoredPrefab(_area.elementsData.Wall, currP + i * VScale * Vector3.up, transform, w);
                    geometric.Add(wall2);
                }

            }

            foreach (var p in pullars)
            {
                var currP = transform.position + p * HScale;
                if (!blacklist.ContainsPillarGround(currP))
                {
                    blacklist.AddPillarGround(currP);
                    var ground = FabricGameObject.InstantiatePillarPrefab(_area.elementsData.GroundPillar, currP, transform);
                    if (_save)
                        SavePoint(ground);
                    geometric.Add(ground);

                }
                if (!blacklist.ContainsPillar(currP))
                {
                    if (walls.Count > 0 || !PrefsGraph.Instant.SettingGraph.pillarAboutWall)
                    {
                        blacklist.AddPillar(currP);
                        for (int i = 0; i < hight; i++)
                        {
                            geometric.Add(FabricGameObject.InstantiatePillarPrefab(_area.elementsData.Pillar, currP + i * VScale * Vector3.up, transform));
                        }

                    }
                }
            }
        }

        private static void SavePoint(GameObject ground)
        {
            ground.layer = 6;
            if (ground.transform.childCount > 0)
            {
                for(int i=0;i< ground.transform.childCount; i++)
                {
                    var child = ground.transform.GetChild(i);
                    child.gameObject.layer = 6;
                }
            }
        }
    }

}
