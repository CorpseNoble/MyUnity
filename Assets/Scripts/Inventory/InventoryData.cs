using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New Player Inventory Data", menuName = "Player Inventory Data", order = 52)]
    public class PlayerInventoryData : ScriptableObject
    {
        public List<PlayerItem> PlayerItem = new List<PlayerItem>();
        public List<PlayerStats<MainStatType>> playerMainStats;
        public List<PlayerStats<SecStatType>> playerSecStats;

        public PlayerItem RHand;
        public PlayerItem LHand;
        public PlayerItem ArmorHead;
        public PlayerItem ArmorUpper;
        public PlayerItem ArmorLower;
        public PlayerItem ArmorArm;
        public PlayerItem ArmorLeg;
        public PlayerItem ArmorBoot;
        public PlayerItem Ring1;
        public PlayerItem Ring2;
        public PlayerItem Collar;

        public void Awake()
        {
            FillStat(out playerMainStats);
            FillStat(out playerSecStats);
        }

        public void FillStat<T>(out List<PlayerStats<T>> playerStats) where T : struct
        {
            playerStats = new List<PlayerStats<T>>();
            var stats = typeof(T).GetEnumValues();

            foreach (T stat in stats)
            {
                var playerstat = new PlayerStats<T>() { Stat = stat };
                playerStats.Add(playerstat);
            }
        }
    }

    [Serializable]
    public class PlayerItem
    {
        public GameItem GameItem
        {
            get
            {
                if (gameItem == null)
                    gameItem = InventoryPrefs.Instant.itemsData.gameItems[GameItemID];

                return gameItem;
            }
        }

        public Grade Grade
        {
            get
            {
                if (grade == null)
                    grade = InventoryPrefs.Instant.gradeData.Grades[GradeID];

                return grade;
            }
        }

        [NonSerialized] private  GameItem gameItem;
        [NonSerialized] private  Grade grade;

        [SerializeField] private int GameItemID = 0;
        [SerializeField] private int GradeID = 0;

        [SerializeField] public int Quantity = 1;
    }
    [Serializable]
    public class PlayerStats<T> where T : struct
    {
        public T Stat;
        public int Value;
    }
}
