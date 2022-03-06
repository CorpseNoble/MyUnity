using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;


namespace GenPr1
{
    public abstract class Monster : HaveDamageScript
    {

        public int level = 1;
        public int levelHp = 100;
        public int levelDamage = 10;

        public float waitForAttack = 10f;
        public float timerForAttack = 0f;
        public float speed = 10f;
        public float angularSpeed = 45f;
        public bool stan = false;
        public float stanTime = 4f;
        public float attackDistance = 1f;

        public GameObject mainObject;
        //  public Animator animator;
        public Materials materials = new Materials();

        protected MeshRenderer meshRenderer;

        public NavMeshAgent navMeshAgent;

        protected float distancTp = 0f;

        public float DistanceTP
        {
            get => distancTp;
            set
            {
                distancTp = value;
                timerForAttack += Time.fixedDeltaTime;
                if (HP > 0)
                    if (readyForAttack)
                    {
                        if (timerForAttack > waitForAttack)
                            Stay = MonsterStay.Attack;
                        else
                            Stay = MonsterStay.Idle;
                    }
                    else
                    {
                        Stay = MonsterStay.Go;
                    }
            }
        }
        public MonsterStay stay;
        public virtual MonsterStay Stay
        {
            get => stay;
            set
            {
                if (!stan)
                {
                    stay = value;
                    switch (value)
                    {
                        case MonsterStay.Idle:
                            navMeshAgent.destination = transform.position;
                            break;
                        case MonsterStay.Go:
                            navMeshAgent.destination = target.transform.position;
                            break;
                        case MonsterStay.Attack:
                            navMeshAgent.destination = transform.position;
                            Attack();
                            break;
                    }
                }
            }
        }
        public override void Start()
        {
            base.Start();
            navMeshAgent.speed = speed / level;
            navMeshAgent.angularSpeed = angularSpeed / level;
            meshRenderer = gameObject.GetComponent<MeshRenderer>();

            meshRenderer.material = materials.noramal;

            maxHP = level * levelHp;
            HP = maxHP;
            damage = level * levelDamage;

            mainObject.transform.localScale *= level;
            mainObject.transform.Translate(Vector3.up / 2 * (level - 1));
            mainObject.name += (" " + level.ToString());
        }

        protected void FixedUpdate()
        {
            if (target != null)
            {
                DistanceTP = Vector3.Distance(this.transform.position, target.transform.position);
            }
            else
            {
                if (Stay != MonsterStay.Idle && Stay != MonsterStay.Spec)
                    Stay = MonsterStay.Idle;
            }
        }
        protected virtual void Attack()
        {
            animator.SetTrigger("Attack");

            meshRenderer.material = materials.takeDamage;
            if (stan)
                stan = false;
            stan = true;
            StartCoroutine(DelayFloat(6f, delegate
            {
                stan = false;
                timerForAttack = 0f;
            }));

        }

        public override void GiveDamege(int damage)
        {
            if (HP > 0)
            {
                Debug.Log("GiveDamege " + damage + " Monster");
                HP -= damage;
                meshRenderer.material = materials.giveDamage;
                if (stan)
                    stan = false;
                Stay = MonsterStay.Idle;
                stan = true;
                StartCoroutine(DelayFloat(stanTime, delegate { stan = false; }));
            }
        }
        public override void Death()
        {
            if (stan)
                stan = false;
            Stay = MonsterStay.Idle;
            stan = true;
            meshRenderer.material = materials.death;
            StartCoroutine(DelayFloat(stanTime, delegate { Destroy(mainObject); }));
        }

        public IEnumerator DelayFloat(float delay, Action action = null)
        {
            yield return new WaitForSeconds(delay);
            if (HP > 0)
                meshRenderer.material = materials.noramal;
            else
                meshRenderer.material = materials.death;
            action?.Invoke();
        }

        [Serializable]
        public class Materials
        {
            public Material noramal;
            public Material death;
            public Material takeDamage;
            public Material giveDamage;
            public Material spec;
        }
    }
}