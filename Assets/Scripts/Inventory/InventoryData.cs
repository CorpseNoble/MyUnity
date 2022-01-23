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

        public Status Status = new Status();
        public Equipment equipment = new Equipment();

        public List<PlayerTrait> PlayerTraits = new List<PlayerTrait>();


    }
    [Serializable]
    public class Status
    {
        public List<PlayerStats<MainStatType>> playerMainStats;
        public List<PlayerStats<SecStatType>> playerSecStats;

        public Status()
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
    public class Equipment
    {
        public List<PlayerItem> PlayerItems = new List<PlayerItem>();

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
    }

    [Serializable]
    public class PlayerTrait
    {
        public Trait Trait
        {
            get
            {
                if (trait == null)
                    trait = InventoryPrefs.Instant.traitData.Traits[TraitID];

                return trait;
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

        [NonSerialized] private Trait trait;
        [NonSerialized] private Grade grade;

        [SerializeField] private int TraitID = 0;
        [SerializeField] private int GradeID = 0;


        [SerializeField] public int Level = 0;
        [SerializeField] public int Experience = 0;
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

        [NonSerialized] private GameItem gameItem;
        [NonSerialized] private Grade grade;

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
