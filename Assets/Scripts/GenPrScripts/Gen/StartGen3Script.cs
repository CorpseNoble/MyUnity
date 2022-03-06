using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


namespace GenPr1
{
    [ExecuteInEditMode]
    public class StartGen3Script : MonoBehaviour
    {
        //public Interect ButtonStart;
        //public Interect ButtonClear;
        //public Interect ButtonDebug;

        [Range(1, 56)] public int wayCount = 10;
        [Range(0, 10)] public int sizeWay = 1;

        [SerializeField] public SelectOnList genVer;
        public List<GenVer> genVers = new List<GenVer>();

        public WayType startWay;

        public NodeScript baseNode;
        public int sizeRoom = 10;
        public List<WayListScript> ways = new List<WayListScript>();
        public WayListScript walls;
        //public WaySecListScript edges;

        private int counter;
        private AllSide allSides = new AllSide();
        private RoomSide[] sides;
        private List<(WayType, SideScript)> wayList = new List<(WayType, SideScript)>();
        private NodeMap nodeMap = new NodeMap();
        private GenVer selected;

        private void Awake()
        {

        }
        private void OnEnable()
        {
            //ButtonStart = new Interect(Gen);
            //ButtonClear = new Interect(ClearAll);
            //ButtonDebug = new Interect(DebugRender);

            genVer = new SelectOnList(genVers);

            sides = allSides.AllSides;

            // baseNode = new GameObject().AddComponent<NodeScript>();
        }
        private void Start()
        {




        }
        //private void OnDrawGizmosSelected()
        //   {
        //       ButStart.Check();
        //       ButClear.Check();
        //       ButDebug.Check();
        //   }
        [ContextMenu("ClearAll")]
        public void ClearAll()
        {
            selected.Clear();

            counter = wayCount * sides.Count();

            foreach (var b in nodeMap.nodes)
                DestroyImmediate(b.gameObject);

            nodeMap = new NodeMap();

        }
        [ContextMenu("Gen")]

        public void Gen()
        {
            //indexGenVer %= genVers.Count;
            selected = genVer.Value as GenVer;
            selected.LeadUp();
            counter = wayCount * sides.Count();
            var startSide = allSides.Forward;
            foreach (var c in ways)
                foreach (var s in c.sides)
                    wayList.Add((c.Way, s));

            var root = Instantiate(baseNode, this.transform.position, Quaternion.identity, this.transform);
            var p = this.transform.position + startSide.vector * sizeRoom;


            nodeMap.root = root;
            nodeMap.nodes.Add(root);

            GenNodeMap(root, startSide, startWay, p, wayCount, sizeWay);

            GenWays();

            UpdateNavMesh();


        }

        private void UpdateNavMesh()
        {
            NavMesh.RemoveAllNavMeshData();

            var nav = NavMeshBuilder.BuildNavMeshData(
                NavMesh.GetSettingsByID(0),
                Collect(),
                new Bounds(
                    Vector3.zero,
                    new Vector3(
                        wayCount * sides.Count() * sizeRoom,
                        wayCount * sides.Count() * sizeRoom,
                        wayCount * sides.Count() * sizeRoom)),
                Vector3.zero,
                Quaternion.identity);

            NavMesh.AddNavMeshData(nav);
        }

        List<NavMeshBuildSource> Collect()
        {
            var nmbs = new List<NavMeshBuildSource>();

            foreach (var n in nodeMap.nodes)
            {

                var mList = n.GetComponentsInChildren<MeshFilter>();

                foreach (var m in mList)
                {

                    if (m.gameObject.isStatic)
                    {
                        if (m.sharedMesh == null)
                            continue;
                        NavMeshBuildSource source = new NavMeshBuildSource()
                        {
                            area = 0,
                            sourceObject = m.sharedMesh,
                            transform = m.transform.localToWorldMatrix,
                            shape = NavMeshBuildSourceShape.Mesh,

                        };
                        nmbs.Add(source);
                    }
                }

            }

            return nmbs;
        }

        private void GenNodeMap(NodeScript PreNode, RoomSide type, WayType side, Vector3 pos, int count, int way)
        {
            counter--;
            var n = Instantiate(baseNode, pos, Quaternion.identity, this.transform);
            n.gameObject.name += count;
            NodeMap.Connect(n, type.revertSide, side, PreNode);
            nodeMap.nodes.Add(n);

            var isDoor = selected.GetDoor;

            WayType st;
            if (isDoor)
                st = WayType.OpenDoor;
            else
                st = WayType.Area;

            Vector3 p;
            if (way > 0)
            {
                way--;
                p = pos + type.vector * sizeRoom;
                var wayOff = nodeMap.nodes.Where(x => x.transform.position == p);
                if (wayOff.Count() == 0)
                {
                    if (count > 0 && counter > 0)
                        GenNodeMap(n, type, st, p, count, way);
                }
                else if (selected.GetLoop)
                {
                    var foundNode = wayOff.First();
                    NodeMap.Connect(n, type.revertSide, st, foundNode);
                }
                //else
                //{
                //    var foundNode = wayOff.First();
                //    NodeMap.Connect(n, type, st, null);
                //}
                return;
            }
            // var next2 = selected.GetParam;

            var types = selected.GetParam2(type);

            count--;
            foreach (var s in types)
                if (s != type.revertSide)
                {
                    p = pos + s.vector * sizeRoom;
                    var wayOff = nodeMap.nodes.Where(x => x.transform.position == p);
                    if (wayOff.Count() == 0)
                    {
                        //if (next2 != 0)
                        if (count > 0 && counter > 0)
                        {
                            //var next = false;

                            //if (s != allSides.Up && s != allSides.Down)
                            //{
                            //    next = next2 == 1;

                            //    if (!next)
                            //    {
                            //        if (s != type && !next)
                            //            next = next2 == 3;
                            //        if (s == type.rightSide && !next)
                            //            next = next2 == 4;
                            //        if (s == type.leftSide && !next)
                            //            next = next2 == 5;
                            //        if ((s == type || s == allSides.Forward) && !next)
                            //            next = next2 == 2;
                            //    }
                            //}
                            //else
                            //{
                            //    next = next2 == 6;

                            //    if (!next)
                            //    {
                            //        if (s == allSides.Down && !next)
                            //            next = next2 == 7;
                            //        if (s == allSides.Up && !next)
                            //            next = next2 == 8;
                            //    }
                            //}


                            //if (next)
                            GenNodeMap(n, s, st, p, count, sizeWay);
                        }
                    }
                    else if (selected.GetLoop)
                    {
                        var foundNode = wayOff.First();
                        NodeMap.Connect(n, s.revertSide, st, foundNode);
                    }
                    //else
                    //{
                    //    var foundNode = wayOff.First();
                    //    NodeMap.Connect(n, s, st, null);
                    //}
                }
        }
        private void GenWays()
        {
            foreach (var n in nodeMap.nodes)
            {
                List<SideScript> positRooms;
                foreach (var c in sides)
                    if (n.sides.TryGetValue(c.side, out var dub))
                    {
                        positRooms = new List<SideScript>(wayList.Where(w => w.Item1 == dub.t && w.Item2.Side == c.side).Select(d => d.Item2));
                        var inv = positRooms.First();
                        if (dub.start)
                            Instantiate(inv, n.transform.position, inv.transform.rotation, n.transform);
                    }
                    else
                    {
                        positRooms = new List<SideScript>(walls.sides.Where(d => d.Side == c.side));
                        var inv = positRooms.First();
                        Instantiate(inv, n.transform.position, inv.transform.rotation, n.transform);
                    }
            }
        }
        //private void GenEdge()
        //{
        //    foreach (var n in nodeMap.nodes)
        //    {
        //        var listFB = SideCast.GetForBack;
        //        foreach (var c in listFB)
        //            if (n.sides.TryGetValue(c, out var dub))
        //                if (dub.t == WayType.Area)
        //                {
        //                    var listRL = SideCast.GetLeftRight;
        //                    foreach (var c1 in listRL)
        //                        if (n.sides.TryGetValue(c1, out var dub1))
        //                            if (dub1.t == WayType.Area)
        //                            {
        //                                bool b = false;

        //                                if (dub.n.sides.TryGetValue(c1, out var dub2))
        //                                    b = dub2.t == WayType.OpenDoor;
        //                                else
        //                                    b = true;

        //                                if (b)
        //                                {
        //                                    var positRooms = new List<SideScript>(edges.sides.Where(e1 => e1.Side == c && e1.SecSide == c1));
        //                                    var inv = positRooms.First();
        //                                    Instantiate(inv, n.transform.position, inv.transform.rotation, n.transform);
        //                                }
        //                            }
        //                }
        //    }
        //}
        [ContextMenu("DebugRender")]

        public void DebugRender()
        {
            foreach (var n in nodeMap.nodes)
                foreach (var n2 in n.sides)
                    Debug.DrawLine(n.transform.position, n2.Value.n.transform.position, Color.white, 10f);
        }

    }
}