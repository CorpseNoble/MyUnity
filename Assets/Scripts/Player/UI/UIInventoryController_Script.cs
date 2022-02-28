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
    public class UIInventoryController_Script : MonoBehaviour
    {
        public PlayerInventoryData inventoryData;

        public GameObject uiGameItemContainer;
        public GameObject uiTraitContainer;
        public UIGameItem_Script uiGameItemPrefab;
        public UITrait_Script uiTraitPrefab;
        public List<UIGameItem_Script> uIGameItems = new List<UIGameItem_Script>();
        public List<UITrait_Script> uITraits = new List<UITrait_Script>();

        public GameObject mainStatContainer;
        public GameObject secStatContainer;
        public GameObject barStatContainer;
        public Dictionary<MainStatType, Text> mainStatTexts;
        public Dictionary<SecStatType, Text> secStatTexts;
        public Dictionary<BarStatType, Text> barStatTexts;
        public Text uiStatPrefab;

        private void Start()
        {
            FillStat(mainStatContainer, inventoryData.status.playerMainStats, out mainStatTexts);
            FillStat(secStatContainer, inventoryData.status.playerSecStats, out secStatTexts);
            FillBarStat(barStatContainer, inventoryData.status.playerBarStats, out barStatTexts);
            RenderTraitInventory();
            RenderItemInventory();
        }
        public void FillStat<T>(GameObject statContainer, List<PlayerStats<T>> playerStats, out Dictionary<T, Text> statTexts) where T : struct
        {
            statTexts = new Dictionary<T, Text>();
            var stats = typeof(T).GetEnumValues();

            foreach (T stat in stats)
            {
                var text = Instantiate(uiStatPrefab, statContainer.transform);
                statTexts.Add(stat, text);
                gameObject.name = stat.ToString();
                var playerStat = inventoryData.status.GetStat(stat, playerStats);
                text.text = stat.ToString() + "\n" + playerStat.value;

            }
        }

        public void FillBarStat<T>(GameObject statContainer, List<PlayerBarStats<T>> playerStats, out Dictionary<T, Text> statTexts) where T : struct
        {
            statTexts = new Dictionary<T, Text>();
            var stats = typeof(T).GetEnumValues();

            foreach (T stat in stats)
            {
                var text = Instantiate(uiStatPrefab, statContainer.transform);
                statTexts.Add(stat, text);
                gameObject.name = stat.ToString();
                var barPlayerStat = inventoryData.status.GetBarStat(stat, playerStats);
                text.text = stat.ToString() + "\n" + barPlayerStat.valueCurrent + "\\" + barPlayerStat.valueMax;
            }
        }

        public void RenderItemInventory()
        {
            foreach (var pitem in inventoryData.playerItems)
            {
                var uiGameItem = Instantiate(uiGameItemPrefab, uiGameItemContainer.transform);
                uiGameItem.equpButtonClick.AddListener(Equip);
            }
        }

        public void RenderTraitInventory()
        {
            foreach (var trait in inventoryData.playerTraits)
            {
                var uiTrait = Instantiate(uiTraitPrefab, uiTraitContainer.transform);
            }
        }

        public void Equip(PlayerItem playerItem)
        {
            if (playerItem is PlayerWeapon playerWeapon)
            {
                inventoryData.equipment.weaponPlace.EqupWeapon(playerWeapon);
                return;
            }
            if (playerItem is PlayerEqupment playerEqupment)
            {
                inventoryData.equipment.armorPlace.EqupArmor(playerEqupment);
                return;
            }
            throw new Exception("unsupported playerItem");
        }
    }


}
