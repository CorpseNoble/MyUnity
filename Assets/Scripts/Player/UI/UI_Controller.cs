using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public PlayerCharacter player;

    public MaxCurrentChangeSlider HP;
    public MaxCurrentChangeSlider SP;
    public MaxCurrentChangeSlider MP;


    public GameObject deadPanel;
    private void Awake()
    {
        HP.Init(100);
        SP.Init(100);
        MP.Init(100);

        player.MaxHealthChanged.AddListener(MaxHPChanged);
        player.HealthChanged.AddListener(HPChanged);
        player.WasDead.AddListener(PlayerIsDead);
    }
    private void MaxHPChanged(AliveController sender, int value)
    {
        HP.CurrentMaxValue = value;
    }
    private void HPChanged(AliveController sender, int value)
    {
        HP.CurrentValue = value;
    }

    public void PlayerIsDead(AliveController sender)
    {
        deadPanel.SetActive(true);
        StartCoroutine(SetAlbedo(deadPanel.GetComponent<Image>()));
    }

    private IEnumerator SetAlbedo(Image image)
    {
        float a = 0f;
        do
        {
            image.color = new Color(0, 0, 0, a);
            yield return new WaitForEndOfFrame();
            a += Time.deltaTime;
        } while (image.color.a < 1);
    }
    // Use this for initialization
    private void Start()
    {

    }

}
