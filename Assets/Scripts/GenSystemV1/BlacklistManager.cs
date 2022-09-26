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

        public BlacklistManager()
        {
            PointBlacklist = new List<Point>();
        }

        public void Add(Point pos)
        {
            PointBlacklist.Add(pos);
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
            return PointBlacklist.Where(c => c.transform.position == pos).Any();
        }
        public Point SelectPoint(Vector3 pos)
        {
            return PointBlacklist.Where(c => c.transform.position == pos).First();
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
        }

        public bool WayCheckClear(Vector3 pos, Vector3 vector, int lenght)
        {

            for (int i = 0; i < lenght; i++)
            {
                if (CheckPos(pos))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// true is blocked
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool CheckPos(Vector3 pos)
        {
            if (Contains(pos))
                return true;
            for (int j = 1; j < PrefsGraph.Instant.SettingGraph.hight; j++)
            {
                if (Contains(pos.StepV(j)))
                    return true;
            }

            return false;
        }

        public bool FullLRCheck(Vector3 pos, Vector3 vector, int lenght, ref Vector3 currentVector)
        {
            if (!WayCheckClear(pos, vector, lenght))
            {
                var left = WayCheckClear(pos, vector.ToLeft(), lenght);
                var right = WayCheckClear(pos, vector.ToRight(), lenght);
                if (left && right)
                {
                    if (PrefsGraph.Instant.SettingGraph.LRPercent.GetValue())
                        currentVector = currentVector.ToLeft();
                    else
                        currentVector = currentVector.ToRight();
                }
                else if (left)
                {
                    currentVector = currentVector.ToLeft();
                }
                else if (right)
                {
                    currentVector = currentVector.ToRight();
                }
                if (!left && !right)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
