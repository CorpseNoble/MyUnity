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

        public GameObject Roof;
        public GameObject Ground;
        public GameObject GroundWallX;
        public GameObject GroundWallY;
        public GameObject GroundPillar;
        
        public PillarPack PillarPack;

        public GameObject Door;
        public GameObject Wall;
        public GameObject WallWindow;

        public GameObject StairsGround;
        public GameObject StairsRoof;
        public GameObject StairsWallR;
        public GameObject StairsWallL;
        public GameObject StairsGroundLWall;
        public GameObject StairsGroundRWall;


        public SpecialPack SpecialPack;
        public List<GameObject> MonsterSpawners = new List<GameObject>();
    }
    [Serializable]
    public class PillarPack
    {
        public GameObject PillarBase1;
        public GameObject PillarBase2;
        public GameObject PillarUp;
        public GameObject PillarDown;
    }
    [Serializable]
    public class SpecialPack
    {
        public GameObject Light;
        public GameObject Chest;
        public GameObject Bonfire;
        public GameObject Lever;
    }
}
