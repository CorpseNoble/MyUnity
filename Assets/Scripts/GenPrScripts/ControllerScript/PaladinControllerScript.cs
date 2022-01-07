using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PaladinControllerScript : PlayerController
{
   // public Animator animator;
    public CumScript mainCamera;

    public GraundScript graund;

    //public int Speed = 1;
    //public int Rotation = 0;
    public bool wait = false;
    public bool OnGraund = false;
    public int NumIdle = 0;

    public float timeWait = 60f;
    public float timeWaiting = 0f;

    private PlayerInputs playerInputs = new PlayerInputs();
    public override void Start()
    {
        base.Start();
        graund.helper += delegate { ControllerMethods.SetBool("OnGraund", graund, animator, ref OnGraund); };
        HP = maxHP;
    }
    private void Update()
    {

        ControllerMethods.SetFloat(playerInputs.GoFront.key, playerInputs.GoBack.key, "Speed", animator);
        ControllerMethods.SetFloat(playerInputs.RotateRight.key, playerInputs.RotateLeft.key, "Rotation", animator);
        ControllerMethods.SetFloat(playerInputs.Run.key, "Run", animator);

        ControllerMethods.SetTrigger(playerInputs.GoUp.key, "Jump", animator);
        ControllerMethods.SetTrigger(playerInputs.Attack.key, "Attack", animator);

        ControllerMethods.SetBool(playerInputs.Block.key, "Block", animator);

        if (!Input.anyKey)
        {
            timeWaiting += Time.deltaTime;
            if (timeWaiting >= timeWait && !wait)
            {
                animator.SetInteger("NumWait", NumIdle);
                NumIdle++;
                NumIdle %= 3;
                animator.SetTrigger("Wait");
                wait = true;
            }
        }
        else
        {
            if (wait)
            {
                animator.SetTrigger("Any Key");
                wait = false;
            }
            timeWaiting = 0f;
        }

        if (Input.GetKeyUp(playerInputs.Target.key))
        {
            if (rangeView.Count > 0)
                if (target == null)
                {
                    if (rangeView.Count > 0)
                    {
                        target = rangeView[0];
                        target.Targeted(true);
                        mainCamera.target = target.targetMask.target;
                    }
                }
                else
                {
                    target.Targeted(false);
                    target = null;
                    mainCamera.target = targetMask.target;
                }
        }
        if (target != null)
        {
            if (rangeView.Count == 0)
            {
                target.Targeted(false);
                target = null;
                mainCamera.target = targetMask.target;
            }
        }
    }

    public override void GiveDamege(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            animator.SetTrigger("Damage");
            HPbar.SetValue(maxHP, HP);
        }
    }
    public override void Death()
    {
        base.Death();
        animator.SetBool("Death", true);
    }
}

public abstract class PlayerController : HaveDamageScript
{
}
