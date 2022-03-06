using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


namespace GenPr1
{
    public class BarScript : MonoBehaviour
    {
        public Image background;
        public Image fillBack;
        public Image fill;

        public Color backgroundColor;
        public Color fillBackColor;
        public Color fillColor;

        public float speedSetCurrentValue = 1f;
        public float delay = 1f;

        private bool f = false;
        private float currentValueFill = 1f;

        private float CurrentValueFill
        {
            get => currentValueFill;
            set
            {
                currentValueFill = value;
                fill.fillAmount = value;
            }
        }

        private float currentValueFillBack = 1f;

        private float CurrentValueFillBack
        {
            get => currentValueFillBack;
            set
            {
                currentValueFillBack = value;
                fillBack.fillAmount = value;
            }
        }
        private void Start()
        {
            background.color = backgroundColor;
            fillBack.color = fillBackColor;
            fill.color = fillColor;
        }
        private void Update()
        {
            if (CurrentValueFill < CurrentValueFillBack)
                if (f)
                    CurrentValueFillBack -= (speedSetCurrentValue * Time.deltaTime);
        }
        public void SetValue(float max, float curr)
        {
            CurrentValueFill = curr / max;
            f = false;
            StartCoroutine(Delay(delay));
        }
        private IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
            f = true;
        }
    }
}
