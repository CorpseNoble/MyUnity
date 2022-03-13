using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Inventory;

namespace Assets.Scripts.Player.UI
{
    public class InventoryController : MonoBehaviour
    {
        public PlayerInventoryData inventoryData;
        public KeyCode key = KeyCode.I;
        public GameObject inventoryPanel;
        public InventoryTabType currentTab = 0;
        public GameObject detailPanel;

        [Header("UI GameItem")]
        public Transform itemContainer;
        public UIGameItem uiGameItemPrefab;
        public UITraitItem uiTraitPrefab;

        [Header("UI Weapon Items")]
        public List<UIGameItem> uIWeaponItems = new List<UIGameItem>();
        [Header("UI Equpment Items")]
        public List<UIGameItem> uIEqupmentItems = new List<UIGameItem>();
        [Header("UI Consumble Items")]
        public List<UIGameItem> uIConsumbleItems = new List<UIGameItem>();
        [Header("UI Other Items")]
        public List<UIGameItem> uIOtherItems = new List<UIGameItem>();
        [Header("UI Trat Items")]
        public List<UITraitItem> uITraitItems = new List<UITraitItem>();

        [Header("UI Stats")]
        public Transform statContainer;
        public Dictionary<MainStatType, UIStatItem> mainStatTexts;
        public Dictionary<SecStatType, UIStatItem> secStatTexts;
        public Dictionary<BarStatType, UIStatItem> barStatTexts;
        public UIStatItem uiStatPrefab;

        public bool InMenu
        {
            get => PlayerGamePrefs.InMenu;
            set
            {
                PlayerGamePrefs.InMenu = value;
                inventoryPanel.SetActive(value);
            }
        }

        public bool InDialog => PlayerGamePrefs.InDialog;

        private void OnEnable()
        {
            inventoryData.status.StatusChanged += StatusUpdated;
        }

        private void OnDisable()
        {
            inventoryData.status.StatusChanged -= StatusUpdated;
        }

        private void Start()
        {
            RenderStatus();
        }


        private void Update()
        {
            if (Input.GetKeyDown(key))
            {
                InMenu = !InMenu;
            }
        }
        public void StatusUpdated(Status status)
        {

        }

        public void RenderStatus()
        {
            FillStat(statContainer, inventoryData.status.playerMainStats, out mainStatTexts);
            FillStat(statContainer, inventoryData.status.playerSecStats, out secStatTexts);
            FillStat(statContainer, inventoryData.status.playerBarStats, out barStatTexts, true);
        }
        public void FillStat<T>(
            Transform statContainer,
            List<Status.PlayerStats<T>> playerStats,
            out Dictionary<T, UIStatItem> statTexts,
            bool bar = false) where T : struct
        {
            statTexts = new Dictionary<T, UIStatItem>();
            var stats = typeof(T).GetEnumValues();

            foreach (T stat in stats)
            {
                var uiPref = Instantiate(uiStatPrefab, statContainer);
                statTexts.Add(stat, uiPref);
                uiPref.gameObject.name = stat.ToString();
                var playerStat = inventoryData.status.GetStat(stat, playerStats);
                if (bar)
                    uiPref.Init(stat.ToString(), playerStat.Value.ToString() + "\\" + playerStat.Value.ToString());

                else
                    uiPref.Init(stat.ToString(), playerStat.Value.ToString());
            }
        }



        public enum InventoryTabType
        {
            Traits,
            Equpments,
            WeaponItems,
            EqupmentItems,
            ConsumbleItems,
            OterItems,
        }
    }
}
