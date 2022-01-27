using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    [CreateAssetMenu(fileName = "New Point Elements Data", menuName = "Point Elements Data", order = 52)]
    public class PointElementsData : ScriptableObject
    {
        public GameObject Ground;
        public GameObject Roof;
        public GameObject GroundPathWay;
        public GameObject GroundPillar;

        public GameObject Wall;
        public GameObject Pillar;

        public GameObject Door;
        public GameObject Lattice;

        public GameObject Light;
        public GameObject Chest;
        public GameObject Bonfire;
        public List<GameObject> MonsterSpawners = new List<GameObject>();
    }
}
