using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.GenSystemV1
{
    public class Point : GraphElement
    {
        public List<GameObject> geometric = new List<GameObject>();
        Area area;
        //0 - floor
        //1-4 - walls

        public override void Generate()
        {
            area = parentElement.parentElement.parentElement as Area;
            var ground = FabricGameObject.InstantiateGroundPrefab(area.elementsData.Ground, transform.position, transform);
            geometric.Add(ground);


            var roof = FabricGameObject.InstantiateGroundPrefab(area.elementsData.Roof, transform.position + (hight-1) * VScale * Vector3.up, transform);
            area.lightPlaces.Add(roof.GetComponent<WallLightScript>().LightPlace);
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
                    geometric.Add(FabricGameObject.InstantiateVectoredPrefab(area.elementsData.GroundPathWay, currP, transform, way));

                }

                walls.Remove(way);
            }
            foreach (var w in walls)
            {
                var currP = transform.position + w * HScale * 0.5f;
                if (!blacklist.ContainsWall(currP))
                {
                    blacklist.AddWall(currP);
                    geometric.Add(FabricGameObject.InstantiateVectoredPrefab(area.elementsData.GroundPathWay, currP, transform, w));
                }

                for (int i = 0; i < hight; i++)
                {
                    var wall2 = FabricGameObject.InstantiateVectoredPrefab(area.elementsData.Wall, currP + i * VScale * Vector3.up, transform, w);
                    geometric.Add(wall2);
                }

            }

            foreach (var p in pullars)
            {
                var currP = transform.position + p * HScale;
                if (!blacklist.ContainsPillarGround(currP))
                {
                    blacklist.AddPillarGround(currP);

                    geometric.Add(FabricGameObject.InstantiatePillarPrefab(area.elementsData.GroundPillar, currP, transform));

                }
                if (!blacklist.ContainsPillar(currP))
                {
                    if (walls.Count > 0 || !PrefsGraph.Instant.SettingGraph.pillarAboutWall)
                    {
                        blacklist.AddPillar(currP);
                        for (int i = 0; i < hight; i++)
                        {
                            geometric.Add(FabricGameObject.InstantiatePillarPrefab(area.elementsData.Pillar, currP + i * VScale * Vector3.up, transform));
                        }

                    }
                }
            }
        }


    }

}
