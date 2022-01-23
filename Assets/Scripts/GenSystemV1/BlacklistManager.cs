using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts.GenSystemV1
{
    [Serializable]
    public class BlacklistManager
    {
        [SerializeField] private List<Point> PointBlacklist;
        [SerializeField] private List<Vector3> pillarBlacklist = new List<Vector3>();
        [SerializeField] private List<Vector3> pillarGoundBlacklist = new List<Vector3>();
        [SerializeField] private List<Vector3> wallBlacklist = new List<Vector3>();

        private IEnumerable<Vector3> PosBlacklist = null;
        public BlacklistManager()
        {
            PointBlacklist = new List<Point>();
            PosBlacklist = PointBlacklist.Select(c => c.transform.position);
        }

        public void Add(Point pos)
        {
            PointBlacklist.Add(pos);
            PosBlacklist = PointBlacklist.Select(c => c.transform.position);
        }

        public void AddPillar(Vector3 pos)
        {
            pillarBlacklist.Add(pos);
        }
        public void AddPillarGround(Vector3 pos)
        {
            pillarGoundBlacklist.Add(pos);
        }
        public void AddWall(Vector3 pos)
        {
            wallBlacklist.Add(pos);
        }

        public bool Contains(Vector3 pos)
        {
            return PosBlacklist.Contains(pos);
        }
        public bool ContainsPillar(Vector3 pos)
        {
            return pillarBlacklist.Contains(pos);
        }
        public bool ContainsPillarGround(Vector3 pos)
        {
            return pillarGoundBlacklist.Contains(pos);
        }
        public bool ContainsWall(Vector3 pos)
        {
            return wallBlacklist.Contains(pos);
        }

        public void Clear()
        {
            PointBlacklist.Clear();
            pillarBlacklist.Clear();
            pillarGoundBlacklist.Clear();
            wallBlacklist.Clear();
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
}
