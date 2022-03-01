using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class SimpleEnemyManager : AliveController
    {
        [SerializeField]
        private GameObject _player;
        [SerializeField]
        [Range(0, 1000)]
        private float _visionDistance = 500;
        [SerializeField]
        [Range(0, 10)]
        private float _attackDistance = 5;
        [SerializeField]
        [Range(0, 10)]
        private float _deathTimer = 0;

        private NavMeshAgent _agent;
        private NavMeshPath _path;
        private EnemyState _state;
        private Animator _anim;
        private bool _stunned = false;

        private AliveController _currentEnemy;

        public AliveController CurrentEnemy
        {
            get => _currentEnemy;
            set => _currentEnemy = value;
        }


        new void Start()
        {
            base.Start();
            acceptor = DamageAcceptor.Enemy;
            _player = GameObject.FindGameObjectWithTag("Player");
            _agent = GetComponent<NavMeshAgent>();
            _agent.isStopped = true;
            _path = new NavMeshPath();
            if (_player != null)
            {
                var position = _player.transform.position;
                _agent.CalculatePath(position, _path);
                _agent.path = _path;
                _agent.isStopped = false;
            }
        }

        private void Update()
        {
            switch (_state)
            {
                case EnemyState.Iddle:
                    Iddle();
                    break;
                case EnemyState.Move:
                    Move();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                default:
                    break;
            }
        }



        protected virtual void Move()
        {
            _agent.isStopped = false;
            var position = _player.transform.position;
            if (_player != null)
            {
                //agent.isStopped = false;
                if ((_player.transform.position - transform.position).magnitude > _visionDistance)
                {
                    _state = EnemyState.Iddle;
                }
                else if ((_player.transform.position - transform.position).magnitude < _attackDistance)
                {
                    _state = EnemyState.Attack;
                }
                else
                {
                    //_anim.SetBool("Walk", true);
                    _agent.CalculatePath(position, _path);
                    _agent.path = _path;
                }
            }
            else if (!_agent.isStopped)
            {
                _agent.isStopped = true;
            }
        }

        protected void Attack()
        {
            _agent.isStopped = true;
            //_anim.SetTrigger("Attack");   //???
            if ((_player.transform.position - transform.position).magnitude > _attackDistance) //&& !stunned)//делать рейкаст и доставать компонент каждый раз???
            {
                _state = EnemyState.Move;
            }
        }

        protected void Iddle()
        {
            _agent.isStopped = true;
            if (_stunned)
            {
                //legushka
            }
            else if (_player != null)
            {
                var position = _player.transform.position;
                if ((position - transform.position).magnitude < _attackDistance)
                {
                    _state = EnemyState.Attack;
                }
                else if ((position - transform.position).magnitude < _visionDistance)
                {
                    _state = EnemyState.Move;
                }
            }
        }


        protected override void Death()
        {
            base.Death();
            Debug.Log("Slame Dead");
            Stun(_deathTimer);
            Destroy(gameObject, _deathTimer);
        }

        private IEnumerator Stun(float time)
        {
            _state = EnemyState.Iddle;
            _stunned = true;
            yield return new WaitForSeconds(time);
            _stunned = false;
        }

    }
}

enum EnemyState
{
    Iddle,
    Attack,
    Move
}