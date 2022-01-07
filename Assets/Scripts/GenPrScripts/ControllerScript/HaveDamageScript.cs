using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HaveDamageScript : HaveHPScript
{
    public int Damage { get => damage; set => damage = value; }
    public int damage = 100;
    public AnimVariable attack = new AnimVariable("Attack", AnimatorControllerParameterType.Trigger);
    public bool readyForAttack = false;
    public HaveDamageScript target;
    public List<HaveDamageScript> rangeView = new List<HaveDamageScript>();

    public TargetMask targetMask = new TargetMask();

    public override void Start()
    {
        base.Start();
        actionsTriggerReactor.Add(TriggerType.TargetTrigger, new Action<LincControllerScript, bool>(TargetTrigerReaction));
        actionsTriggerReactor.Add(TriggerType.ReadyAttackTrigger, new Action<LincControllerScript, bool>(ReadyTrigerReaction));
    }

    public void Targeted(bool targeted)
    {
        targetMask.MeshRenderer.material = targeted ? targetMask.TargetedMaterial : targetMask.NormalMaterial;
    }
    public void TargetTrigerReaction(LincControllerScript linc, bool enter)
    {
        if (linc.controller.gameObject.tag != tag)
            if (enter)
            {
                linc.controller.rangeView.Add(this);
                if (linc.controller.target == null)
                    linc.controller.target = linc.controller.rangeView[0];
            }
            else
            {
                linc.controller.rangeView.Remove(this);
            }
    }
    public void ReadyTrigerReaction(LincControllerScript linc, bool enter)
    {
        linc.controller.readyForAttack = enter;
    }
}
