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
    public class UITraitItem : MonoBehaviour
    {
        public string lvlStyle = "LV: ";
        public Text traitName;
        public Text traitLV;
        public Image background;
        public Slider traitProgres;

        public PlayerTrait playerTrait;

        private void Start()
        {
            traitName.text = playerTrait.Trait.name;
            traitLV.text = lvlStyle + playerTrait.level.ToString();
            traitProgres.maxValue = playerTrait.Trait.expirience;
            traitProgres.value = playerTrait.experience;
        }
    }
}
