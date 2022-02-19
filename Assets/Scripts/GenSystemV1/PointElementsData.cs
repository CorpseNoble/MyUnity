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
        [Range(1, 100)] public float HorScale = 6;
        [Range(1, 100)] public float VerScale = 4;

        public GameObject Ground;
        public GameObject Roof;
        public GameObject GroundPathWay;
        public GameObject GroundPillar;

        public GameObject Wall;
        public GameObject PillarBase1;
        public GameObject PillarBase2;
        public GameObject PillarUp;
        public GameObject PillarDown;

        public GameObject Door;
        public GameObject Lattice;

        public GameObject Light;
        public GameObject Chest;
        public GameObject Bonfire;
        public List<GameObject> MonsterSpawners = new List<GameObject>();
    }
}
