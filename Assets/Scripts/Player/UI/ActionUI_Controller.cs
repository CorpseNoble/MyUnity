using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Inventory;

namespace Assets.Scripts.Player.UI
{
    public class ActionUI_Controller : MonoBehaviour
    {
        public PlayerInventoryData playerInventory;


        public Dictionary<BarStatType, MaxCurrentChangeSlider> barStats;
        public MaxCurrentChangeSlider prefab;

        public GameObject deadPanel;
        private void Awake()
        {
            barStats = new Dictionary<BarStatType, MaxCurrentChangeSlider>();

            var barStatTyps = typeof(BarStatType).GetEnumValues();

            foreach (BarStatType bs in barStatTyps)
            {
                var barstat = Instantiate(prefab, transform);
                barStats.Add(bs, barstat);
                var color = Color.red;
                if (bs == BarStatType.MP)
                    color = Color.blue;
                if (bs == BarStatType.FP)
                    color = Color.green;
                if (bs == BarStatType.Fatigue)
                    color = Color.cyan;
                if (bs == BarStatType.Stress)
                    color = Color.magenta;
                barstat.Init(playerInventory.status[bs].Value, color);
            }
        }
        public void BarStatChanged(BarStatType barStat, int value)
        {
            barStats[barStat].CurrentValue = value;
        }

        public void PlayerIsDead()
        {
            deadPanel.SetActive(true);
            StartCoroutine(SetAlbedo(deadPanel.GetComponent<Image>()));
        }

        private IEnumerator SetAlbedo(Image image)
        {
            float a = 0f;
            do
            {
                image.color = new Color(0, 0, 0, a);
                yield return new WaitForEndOfFrame();
                a += Time.deltaTime;
            } while (image.color.a < 1);
        }
    }
}
