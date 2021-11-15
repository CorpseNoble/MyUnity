using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxCurrentChangeSlider : MonoBehaviour
{
    public float MaxValue
    {
        get => maxValue;
        set
        {
            maxValue = value;
            Max.maxValue = value;
        }
    }
    public float CurrentMaxValue
    {
        get => currentMaxValue;
        set
        {
            currentMaxValue = value;
            Max.value = value;
            Current.maxValue = value;
            Change.maxValue = value;
        }
    }
    public float CurrentValue
    {
        get => currentValue;
        set
        {
            if (currentValue < value)
            {

            }
            else
            {

            }
            currentValue = value;
        }
    }
    public Slider Change;
    public Slider Current;
    public Slider Max;
    [SerializeField] private float currentMaxValue = 1000f;
    [SerializeField] private float maxValue = 1000f;
    [SerializeField] private float currentValue = 1000f;
    public float delay = 0.5f;
    public float hight = 10f;
    public Color backgroundColor = Color.black;
    public Color currentColor = Color.red;
    public Color changeColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
