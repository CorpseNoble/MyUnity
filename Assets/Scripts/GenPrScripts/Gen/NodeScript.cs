using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GenPr1
{
    public class NodeScript : MonoBehaviour
    {
        public Dictionary<SideType, (WayType t, NodeScript n, bool start)> sides = new Dictionary<SideType, (WayType t, NodeScript n, bool start)>();

        public bool isScene;
    }

    public class NodeMap
    {
        public NodeScript root;

        public List<NodeScript> nodes = new List<NodeScript>();

        public NodeMap()
        {
            nodes = new List<NodeScript>();
        }


        public static void Connect(NodeScript fromN, RoomSide t, WayType s, NodeScript toN)
        {
            SideType conT = SideType.Forward;
            try
            {
                conT = t.revertSide.side;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            if (t == null)
                Debug.LogError("t = null");
            else if (t.revertSide == null)
                Debug.LogError("t.revertSide = null");

            try
            {
                fromN.sides.Add(t.side, (s, toN, true));
            }
            catch
            {
                Debug.Log("SideType " + t.side + " WayType " + s);
            }
            try
            {
                toN.sides.Add(conT, (s, fromN, false));
            }
            catch
            {
                Debug.Log("SideType " + t.side + " WayType " + s);
            }
        }
    }
}