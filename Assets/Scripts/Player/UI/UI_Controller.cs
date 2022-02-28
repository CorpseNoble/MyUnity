using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Inventory;

namespace Assets.Scripts.Player.UI
{
    public class UI_Controller : MonoBehaviour
    {
        public PlayerInventoryData playerInventory;

        public MaxCurrentChangeSlider HP;
        public MaxCurrentChangeSlider SP;
        public MaxCurrentChangeSlider MP;
        public MaxCurrentChangeSlider pain;
        public MaxCurrentChangeSlider fatigue;
        public MaxCurrentChangeSlider stress;


        public GameObject deadPanel;
        private void Awake()
        {
            HP.Init(playerInventory.status.HP.valueMax);
            SP.Init(playerInventory.status.SP.valueMax);
            MP.Init(playerInventory.status.MP.valueMax);
            pain.Init(playerInventory.status.Pain.valueMax);
            fatigue.Init(playerInventory.status.Fatigue.valueMax);
            stress.Init(playerInventory.status.Stress.valueMax);

            //player.MaxHealthChanged.AddListener(MaxHPChanged);
            //player.HealthChanged.AddListener(HPChanged);
            //player.WasDead.AddListener(PlayerIsDead);
        }
        public void MaxHPChanged(AliveController sender, int value)
        {
            HP.CurrentMaxValue = value;
        }
        public void HPChanged(AliveController sender, int value)
        {
            HP.CurrentValue = value;
        }

        public void PlayerIsDead(AliveController sender)
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
        // Use this for initialization
        private void Start()
        {

        }

    }
}
