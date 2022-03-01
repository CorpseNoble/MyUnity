using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
using UnityEngine;
[RequireComponent(typeof(SimpleEnemyManager))]
public class Attack : MonoBehaviour/* Action*/
{
    [SerializeField]
    private DamageZone _damageZone;
    
    private SimpleEnemyManager _manager;

    private void Start()
    {
        _manager = GetComponent<SimpleEnemyManager>();
    }

    public /*ovverride*/ void Action()
    {
        _damageZone.GiveDamage(_manager.);
        
        
    }
    
    
}
