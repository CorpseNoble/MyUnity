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
    private List<Vector3> PosBlacklist;

    public BlacklistManager()
    {
        PosBlacklist = new List<Vector3>();
    }

    public void Add(Vector3 pos)
    {
        PosBlacklist.Add(pos);
    }

    public bool Contains(Vector3 pos)
    {
        return PosBlacklist.Contains(pos);
    }

    public void Clear()
    {
        PosBlacklist.Clear();
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
