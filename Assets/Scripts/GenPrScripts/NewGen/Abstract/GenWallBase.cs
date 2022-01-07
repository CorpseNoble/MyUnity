using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
public abstract class GenWallBase : MonoBehaviour
{
    public List<WayListScript> ways = new List<WayListScript>();
    public WayListScript walls;
    protected List<(WayType, SideScript)> wayList = new List<(WayType, SideScript)>();
    protected RoomSide[] sides;
    protected AllSide allSides = new AllSide();
    protected void OnEnable()
    {
        if (wayList.Count() == 0)
            foreach (var c in ways)
                foreach (var s in c.sides)
                    wayList.Add((c.Way, s));

        if (sides == null)
            sides = allSides.AllSides;
    }

    public virtual void GenWay(NodeMap nodeMap)
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
}
