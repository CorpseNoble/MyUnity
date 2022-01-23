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
            // Create QUAD and give a f*ck
            //geometric.Add(FabricGameObject.CreateQuad(transform.position, transform));

            geometric.Add(FabricGameObject.InstantiateGroundPrefab(area.elementsData.GroundRoof, transform.position, transform));
            //geometric.Add(FabricGameObject.InstantiateRoofPrefab(area.elementsData.Roof, transform.position, transform));
            blacklist.Add(this);

            //var aboutPos = transform.position.About();

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
            var hasWall = false;
            foreach (var ce in connectElements)
            {
                if (ce.GetConnect(this).connectElements.Count < 4)
                    hasWall = true;

                var way = (ce.GetConnect(this).transform.position - transform.position).normalized;
                var currP = transform.position + way * scale * 0.5f;
                if (!ce.instanted)
                {
                    ce.instanted = true;
                    geometric.Add(FabricGameObject.InstantiateVectoredPrefab(area.elementsData.GroundPathWay, currP, transform, way));
                }

                walls.Remove(way);
            }
            foreach (var w in walls)
            {
                var currP = transform.position + w * scale * 0.5f;
                if (!blacklist.ContainsWall(currP))
                {
                    blacklist.AddWall(currP);
                    geometric.Add(FabricGameObject.InstantiateVectoredPrefab(area.elementsData.GroundPathWay, currP, transform, w));
                }

                var wall = FabricGameObject.InstantiateVectoredPrefab(area.elementsData.Wall, currP, transform, w);
                geometric.Add(wall);
                area.LightPlaces.Add(wall.GetComponent<WallLightScript>().LightPlace);

            }

            foreach (var p in pullars)
            {
                var currP = transform.position + p * scale;
                if (!blacklist.ContainsPillarGround(currP))
                {
                    blacklist.AddPillarGround(currP);

                    geometric.Add(FabricGameObject.InstantiatePillarPrefab(area.elementsData.GroundPillar, currP, transform));

                }
                if (!blacklist.ContainsPillar(currP))
                {
                    if (walls.Count > 0 || !PrefsGraph.Instant.SettingGraph.PillarAboutWall)
                    {
                        geometric.Add(FabricGameObject.InstantiatePillarPrefab(area.elementsData.Pillar, currP, transform));
                        blacklist.AddPillar(currP);

                    }
                }
            }
        }


    }

}
