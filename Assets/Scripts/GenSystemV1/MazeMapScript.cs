using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.GenSystemV1
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(NavMeshSurface))]
    [RequireComponent(typeof(Area))]
    public class MazeMapScript : MonoBehaviour
    {

        public BlacklistManager blacklist;

        private NavMeshSurface meshSurface;
        private Area area;

        private void Start()
        {
            if (area.subElements.Count() <= 0)
                ReGenMaze();
        }
        void OnEnable()
        {

            blacklist ??= new BlacklistManager();
            meshSurface = GetComponent<NavMeshSurface>();
            area = GetComponent<Area>();

        }
        private void GenStepWall()
        {

            var points = new List<GraphElement>();
            foreach (var seZ in area.subElements)
            {
                foreach (var seR in seZ.subElements)
                {
                    points.AddRange(seR.subElements);

                }
            }
            foreach (Point p in points)
            {
                p.GenWalls();
            }
        }
        [ContextMenu("ReGenMaze")]
        public void ReGenMaze()
        {
            PrefsGraph.Instant.SettingGraph.UpdateSeed();
            Gen();
        }
        [ContextMenu("GenNewMaze")]
        public void GenNewMaze()
        {
            PrefsGraph.Instant.SettingGraph.GenerateSeed();
            Gen();
        }
        private void Gen()
        {

            Clear();
            area.blacklist = blacklist;
            area.Generate();
            area.GenLight();
            area.GenRoomEntry();
            GenStepWall();
            meshSurface.BuildNavMesh();
        }
        //public NavMeshDataInstance dataInstance = new NavMeshDataInstance();
        //public NavMeshData data;
        //public LayerMask buildmask;

        //[ContextMenu("BuildNavMesh")]
        //public void BuildNavMesh()
        //{
        //    dataInstance.Remove();

        //   // var boundsZero = new Bounds();
        //   // var bounds1000 = new Bounds(Vector3.zero, new Vector3(1000, 1000, 1000));
        //    var markups = new List<NavMeshBuildMarkup>();
        //    var sources = new List<NavMeshBuildSource>();

        //    NavMeshBuilder.CollectSources(
        //        root: transform,
        //        buildmask,
        //        NavMeshCollectGeometry.PhysicsColliders,
        //        defaultArea: 0,
        //        markups,
        //        sources);

        //    data = NavMeshBuilder.BuildNavMeshData(
        //        NavMesh.GetSettingsByID(0),
        //        sources,
        //        new Bounds(),
        //        transform.position,
        //        transform.rotation);

        //    if (data != null)
        //    {
        //        data.name = gameObject.name;
        //        dataInstance.Remove();
        //        dataInstance = new NavMeshDataInstance();
        //        dataInstance = NavMesh.AddNavMeshData(data, transform.position, transform.rotation);
        //        dataInstance.owner = this;
        //    }
        //    Debug.Log("dataInstance.valid: " + dataInstance.valid);
        //}
        [ContextMenu("Clear")]
        public void Clear()
        {
            foreach (var se in area.subElements)
            {

                //Destroy(se.gameObject);
                DestroyImmediate(se.gameObject);
            }
            area.subElements.Clear();
            area.rootElement = null;
            blacklist.Clear();
            NavMesh.RemoveAllNavMeshData();
        }

    }
}
