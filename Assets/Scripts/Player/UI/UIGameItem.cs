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
    public class UIGameItem : MonoBehaviour
    {
        public Image itemIcon;
        public Text itemName;
        public Text itemQty;
        public Image background;

        public PlayerItem playerItem;

        private void Start()
        {
            itemIcon.sprite = playerItem.GameItem.sprite;
            itemName.text = playerItem.GameItem.name;
            itemQty.text = playerItem.quantity.ToString();
        }
    }
}
