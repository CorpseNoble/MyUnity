using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HaveHPScript : MonoBehaviour
{
    public BarScript HPbar;
    public Animator animator;
    public AnimVariable death = new AnimVariable("Death", AnimatorControllerParameterType.Trigger);
    public AnimVariable giveDamege = new AnimVariable("Damage", AnimatorControllerParameterType.Trigger);
    public int maxHP = 100;
    public int HP
    { get => hP; set { hP = value; if (value <= 0) Death(); } }
    public int hP;

    public Dictionary<TriggerType, Action<LincControllerScript, bool>> actionsTriggerReactor = new Dictionary<TriggerType, Action<LincControllerScript, bool>>();
    public AudioSource audioSource;

    public virtual void Start()
    {
        actionsTriggerReactor.Add(TriggerType.AttackTrigger, new Action<LincControllerScript, bool>(AttackTrigerReaction));
    }

    public void OnTriggerEnter(Collider other)
    {
        TrigegerReaction(other);
    }

    public void OnTriggerExit(Collider other)
    {
        TrigegerReaction(other, false);
    }
    public virtual void TrigegerReaction(Collider other, bool enter = true)
    {
        if (other.gameObject.GetComponentSafe(out LincControllerScript linc))
            if (linc.controller != this)
                actionsTriggerReactor[linc.triggerType].Invoke(linc, enter);
    }
    public void AttackTrigerReaction(LincControllerScript linc, bool enter)
    {
        if (enter) GiveDamege(linc.controller.Damage);
    }
    public virtual void GiveDamege(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            HPbar?.SetValue(maxHP, HP);
            giveDamege.SetVariable(animator, source: audioSource);
        }
    }
    public virtual void Death()
    {
        death.SetVariable(animator, source: audioSource);
    }

}
