using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class KatanaUseController : MonoBehaviour
{
    public Animator animator;
    public string Attack = "Attack";
    public string OnOff = "OnOff";
    public string SpeedMulti = "SpeedMulti";
    public float SpeedMultiPerCombo = 0.4f;
    public int MaxCombo = 10;
    [Range(0.1f, 1f)] public float comboMulti = 0.1f;
    public UnityEvent ComboUpHandler;
    public UnityEvent ComboEndHandler;
    public AudioSource audioSource;
    public bool OnOffState
    {
        get => onOffState;
        set
        {
            onOffState = value;
            animator.SetTrigger(OnOff);
        }
    }
    public float SpeedMultiplicator
    {
        get => speedMultiplicator;
        set
        {
            speedMultiplicator = value;
            animator.SetFloat(SpeedMulti, value);
        }
    }

    public bool LMB
    {
        get => lMB;
        set
        {
            lMB = value;
            {
                animator.SetBool(Attack, value);
            }
        }
    }
    public bool QKey
    {
        get => qKey;
        set
        {
            qKey = value;
            if (value)
                OnOffState = !OnOffState;
        }
    }

    public int ComboCount
    {
        get => comboCount;
        set
        {
            if (value <= MaxCombo)
            {
                comboCount = value;
                SpeedMultiplicator = 1 + value * SpeedMultiPerCombo;
            }
        }
    }
    [SerializeField] private bool lMB = false;
    [SerializeField] private bool qKey = false;
    [SerializeField] private float speedMultiplicator = 1;
    [SerializeField] private int comboCount = 0;
    [SerializeField] private bool onOffState = false;
    private void Start()
    {
        if (animator == null)
            if (gameObject.TryGetComponent(out Animator anim))
                animator = anim;

        ComboEndHandler.AddListener(ComboEnd);
        ComboUpHandler.AddListener(ComboUp);
    }
    private void Update()
    {
        LMB = Input.GetKey(KeyCode.Mouse0);
        QKey = Input.GetKeyDown(KeyCode.Q);

    }

    private void ComboUp()
    {
        ComboCount++;
    }

    private void ComboEnd()
    {
        ComboCount = 0;
    }
    public DamageZone getDamage;
    private void AttackStart(float multi = 1)
    {
        getDamage.Multi = multi + ComboCount * comboMulti;
    }

    private void AttackEnd()
    {
        getDamage.Multi = 0;
        getDamage.EndHit(out int countHit);
        ComboCount += countHit;
    }
}
