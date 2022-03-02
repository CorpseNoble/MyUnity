using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
using UnityEngine;

[RequireComponent(typeof(SimpleEnemyManager))]
public class Attack : MonoBehaviour /* Action*/
{
    [SerializeField] private DamageZone _damageZone;

    [SerializeField]  private Collider _collider;

    private SimpleEnemyManager _manager;

    [SerializeField][Range(0,10f)]private float _attackDelay =5f;

    private void Start()
    {
        _manager = GetComponent<SimpleEnemyManager>(); 
        _collider.enabled = false;
        //StartCoroutine();
    }

    public /*ovverride*/ void Action()
    {
        _collider.enabled = true;
        StartCoroutine(AttackCoroutine());
    }


    public IEnumerator AttackCoroutine()
    {
        while (_collider.enabled)
        {
            yield return new WaitForSeconds(_attackDelay);
            _damageZone.EndHit(out _);
        }
    }

    public /*ovverride*/ void EndAction()
    {
        _collider.enabled = false;
        _damageZone.EndHit(out _);
    }
}