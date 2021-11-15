using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class BlacklistManager
{
    [SerializeField]
    private List<Point> PointBlacklist;
    private List<Vector3> pullarBlacklist;
    private IEnumerable<Vector3> PosBlacklist = null;
    public BlacklistManager()
    {
        PointBlacklist = new List<Point>();
        pullarBlacklist = new List<Vector3>();
        PosBlacklist = PointBlacklist.Select(c => c.transform.position);
    }

    public void Add(Point pos)
    {
        PointBlacklist.Add(pos);
        PosBlacklist = PointBlacklist.Select(c => c.transform.position);
    }

    public void AddPullar(Vector3 pos)
    {
        pullarBlacklist.Add(pos);
    }

    public bool Contains(Vector3 pos)
    {
        return PosBlacklist.Contains(pos);
    }
    public bool ContainsPullar(Vector3 pos)
    {
        return pullarBlacklist.Contains(pos);
    }

    public void Clear()
    {
        PointBlacklist.Clear();
        pullarBlacklist.Clear();
        PosBlacklist = PointBlacklist.Select(c => c.transform.position);
    }

    public bool WayCheckClear(Vector3 pos, Vector3 vector, int lenght)
    {

        for (int i = 0; i < lenght; i++)
        {
            if (PosBlacklist.Contains(pos) && i < lenght / 2)
                return false;

            pos += vector;
        }
        return true;
    }
}
