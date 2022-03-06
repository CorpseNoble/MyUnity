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
    public class UITrait_Script : MonoBehaviour
    {
        public Text traitName;
        public Text traitDiscription;
        public Text traitLV;
        public Image background;
        public Slider traitProgres;

        public PlayerTrait playerTrait;

        public void SetUp()
        {
            traitName.text = playerTrait.Trait.name;
            traitDiscription.text = playerTrait.Trait.discription;
            background.color = playerTrait.Grade.color;
        }
    }
}
