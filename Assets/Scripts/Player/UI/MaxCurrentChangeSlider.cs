using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxCurrentChangeSlider : MonoBehaviour
{
    public float MaxMaxMaxValue = 1000f;
    public float changeMilti = 10f;
    public float MaxMaxValue
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
            if (currentValue > value)
            {
                currentDelay = delay;
                if (Current.value == Change.value)
                    StartCoroutine(OnChangeCurrentValue());
            }
            else
            {
                Change.value = value;
            }
            currentValue = value;
            Current.value = value;
        }
    }
    public Slider Change;
    public Slider Current;
    public Slider Max;
    [SerializeField] private float currentMaxValue = 1000f;
    [SerializeField] private float maxValue = 1000f;
    [SerializeField] private float currentValue = 1000f;
    public float delay = 0.2f;
    public float currentDelay = 0f;
    public Image imageBackgroung;
    public Image imageCurrent;
    public Image imageChange;
    public Color backgroundColor = Color.black;
    public Color currentColor = Color.red;
    public Color changeColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(float maxVal)
    {
        MaxMaxValue = MaxMaxMaxValue;
        CurrentMaxValue = maxVal;
        CurrentValue = maxVal;

        imageBackgroung.color = backgroundColor;
        imageChange.color = changeColor;
        imageCurrent.color = currentColor;
    }

    [ContextMenu("Init500")]
    public void Init500()
    {
        Init(500);
    }
    public IEnumerator OnChangeCurrentValue()
    {
        do
        {
            
            yield return new WaitForFixedUpdate();

            if (currentDelay > 0)
            {
                currentDelay -= Time.fixedDeltaTime;
            }
            else
            {
                Change.value -= Time.fixedDeltaTime * changeMilti;
            }
        } while (Current.value < Change.value);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
