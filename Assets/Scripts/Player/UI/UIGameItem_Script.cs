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
    public class UIGameItem_Script : MonoBehaviour
    {
        public Image itemIcon;
        public Text itemName;
        public Text itemDiscription;
        public Image background;
        public Button equpButton;

        public PlayerItem playerItem;
        public UnityEngine.Events.UnityEvent<PlayerItem> equpButtonClick;

        private void Start()
        {
            equpButton.onClick.AddListener(() => EqupButton_Click());
            if(playerItem is PlayerOther || playerItem is PlayerConsumble)
            {
                equpButton.enabled = false;
            }
        }

        public void EqupButton_Click()
        {
            equpButtonClick?.Invoke(playerItem);
        }
    }
}
