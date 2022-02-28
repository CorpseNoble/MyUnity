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
        public Image background;

        public PlayerTrait playerTrait;
    }
}
