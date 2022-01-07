using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class SlameControllerScript : Monster
{
    public float waitForUnity = 30f;
    public float timerForUnity = 0f;
    public SlameType slameType;

    public SlameControllerScript boss;
    public List<SlameControllerScript> satellites = new List<SlameControllerScript>();

    private new void Start()
    {
        base.Start();
        if (level > 1)
        {
            slameType = SlameType.Boss;
        }
        else
        {
            slameType = SlameType.Satellite;
        }
    }
    public override MonsterStay Stay
    {
        get => stay;
        set
        {
            base.Stay = value;
            if (!stan)
                if (value == MonsterStay.Spec)
                {
                    switch (slameType)
                    {
                        case SlameType.Boss:
                            break;
                        case SlameType.Satellite:
                            if (boss != null)
                                navMeshAgent.destination = boss.transform.position;
                            break;
                    }
                }
        }
    }
    private new void FixedUpdate()
    {
        base.FixedUpdate();
        if ((satellites.Count > 0 || boss != null) && Stay == MonsterStay.Idle)
        {
            timerForUnity += Time.fixedDeltaTime;
            if (timerForUnity % waitForUnity > 1)
            {
                Stay = MonsterStay.Spec;
                meshRenderer.material = materials.spec;
            }
        }
    }
}


