using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Inventory;

namespace Assets.Scripts.Player
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private AliveController aliveController;
        public Slider slider;

        private void Start()
        {
            if (aliveController == null)
                if (this.TryGetComponent(out aliveController))
                {
                    slider.maxValue = aliveController.MaxHealth;
                    HealthChanged(BarStatType.HP, aliveController.MaxHealth);
                    aliveController.HealthChanged.AddListener(HealthChanged);
                }
        }

        private void HealthChanged(BarStatType sender, int value)
        {
            slider.value = value;
        }
    }
}
