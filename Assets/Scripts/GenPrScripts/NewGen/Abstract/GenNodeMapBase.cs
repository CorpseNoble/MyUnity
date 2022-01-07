using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
public abstract class GenNodeMapBase : MonoBehaviour
{
    public GenVer genVer;

    [Range(1, 100)] public int sizeMaze = 10;
    [Range(0, 10)] public int pathLength = 1;
    [Tooltip("distance between rooms")] public int roomDist = 5;
    public WayType startWay;

    protected NodeScript baseNode;
    protected int counter;
    protected NodeMap nodeMap = new NodeMap();
    protected AllSide allSides = new AllSide();
    protected RoomSide[] sides;

    protected void OnEnable()
    {
        if (baseNode == null)
            baseNode = new GameObject().AddComponent<NodeScript>();

        if (sides == null)
            sides = allSides.AllSides;
    }
    public virtual NodeMap GenerateNodeMap()
    {
        var startSide = allSides.Forward;
        var root = Instantiate(baseNode, this.transform.position, Quaternion.identity, this.transform);
        var p = this.transform.position + startSide.vector * roomDist;
        counter = sizeMaze * (pathLength + 1) * roomDist;

        genVer.Clear();
        genVer.LeadUp();

        nodeMap = new NodeMap();
        nodeMap.root = root;
        nodeMap.nodes.Add(root);

        GenNodeMap(root, startSide, startWay, p, sizeMaze, pathLength);

        return nodeMap;
    }


    protected virtual void GenNodeMap(NodeScript PreNode, RoomSide type, WayType side, Vector3 pos, int count, int way)
    {
        counter--;
        var n = Instantiate(baseNode, pos, Quaternion.identity, this.transform);
        n.gameObject.name += count;
        NodeMap.Connect(n, type.revertSide, side, PreNode);
        nodeMap.nodes.Add(n);

        var isDoor = genVer.GetDoor;

        WayType st;
        if (isDoor)
            st = WayType.OpenDoor;
        else
            st = WayType.Area;

        Vector3 p;
        if (way > 0)
        {
            way--;
            p = pos + type.vector * roomDist;
            var wayOff = nodeMap.nodes.Where(x => x.transform.position == p);
            if (wayOff.Count() == 0)
            {
                if (count > 0 && counter > 0)
                    GenNodeMap(n, type, st, p, count, way);
            }
            else if (genVer.GetLoop)
            {
                var foundNode = wayOff.First();
                NodeMap.Connect(n, type.revertSide, st, foundNode);
            }

            return;
        }

        var types = genVer.GetParam2(type);

        count--;
        foreach (var s in types)
            if (s != type.revertSide)
            {
                p = pos + s.vector * roomDist;
                var wayOff = nodeMap.nodes.Where(x => x.transform.position == p);
                if (wayOff.Count() == 0)
                {
                    if (count > 0 && counter > 0)
                    {
                        GenNodeMap(n, s, st, p, count, pathLength);
                    }
                }
                else if (genVer.GetLoop)
                {
                    var foundNode = wayOff.First();
                    NodeMap.Connect(n, s.revertSide, st, foundNode);
                }

            }
    }
}
