using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player.UI
{
    public class UIStatItem : MonoBehaviour
    {
        public Text textName;
        public Text textValue;

        public void Init(string name, string value)
        {
            textName.text = name;
            textValue.text = value;
        }
    }
}
