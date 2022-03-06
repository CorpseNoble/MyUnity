using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GenPr1
{
    public class MovementScript : MonoBehaviour
    {
        public Animator animator;
        public HaveDamageScript haveDamage;
        public GraundScript graund;
        public new Rigidbody rigidbody;
        public GameObject mainCamera;
        [Range(0, 50)]
        public float JumpForce = 25;
        [Range(1, 3)]
        public float RunMulti = 2;
        [Range(0, 10)]
        public float SpeedZ = 5f;
        [Range(0, 10)]
        public float SpeedX = 5f;

        [Range(0, 10)]
        public float SpeedH = 5f;
        [Range(0, 10)]
        public float SpeedV = 5f;

        public AnimVariable ForwardBack = new AnimVariable("SpeedZ", AnimatorControllerParameterType.Float);
        public AnimVariable LeftRight = new AnimVariable("SpeedX", AnimatorControllerParameterType.Float);
        public AnimVariable Run = new AnimVariable("Run", AnimatorControllerParameterType.Float);

        public AnimVariable Jump = new AnimVariable("Jump", AnimatorControllerParameterType.Trigger);
        public AnimVariable OnGraund = new AnimVariable("OnGraund", AnimatorControllerParameterType.Bool);

        public AudioClip goClip;
        public bool onGraund = false;
        protected PlayerInputs playerInputs = new PlayerInputs();
        protected Vector3 direct = Vector3.zero;
        protected void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            graund.helper += delegate
            {
                if (graund.OnGraund != onGraund)
                {
                    onGraund = graund.OnGraund;
                    OnGraund.SetVariable(animator, onGraund ? 1 : 0);
                }
            };
        }

        protected void Update()
        {
            var h = Input.GetAxis("Mouse X") * SpeedH;

            if (h != 0)
            {
                transform.Rotate(transform.up, h);
            }

            var v = Input.GetAxis("Mouse Y") * SpeedV;

            if (v != 0)
            {
                var vx = -v + mainCamera.transform.localRotation.eulerAngles.x;
                vx = Mathf.Clamp(vx, 30, 90);
                mainCamera.transform.localRotation = Quaternion.Euler(vx, 0, 0);
            }
            direct = Vector3.zero;
            ControllerMethods.ButtonDownTwo(playerInputs.GoFront.key, playerInputs.GoBack.key, ForwardBack, animator,
                actionDownPlus: delegate
                {
                    direct += gameObject.transform.forward * SpeedZ * Time.deltaTime;
                },
                actionDownMinus: delegate
                {
                    direct += (gameObject.transform.forward * -SpeedZ * Time.deltaTime);
                });

            ControllerMethods.ButtonDownTwo(playerInputs.GoRight.key, playerInputs.GoLeft.key, LeftRight, animator,
                actionDownPlus: delegate
                {
                    direct += (gameObject.transform.right * SpeedX * Time.deltaTime);
                },
                actionDownMinus: delegate
                {
                    direct += (gameObject.transform.right * -SpeedX * Time.deltaTime);
                });

            ControllerMethods.ButtonDownOne(playerInputs.Run.key, Run, animator,
                actionDown: delegate
                {
                    direct *= (RunMulti);
                });
            ControllerMethods.ButtonDownOneAudio(playerInputs.GoUp.key, Jump, animator, haveDamage.audioSource,
                actionDown: delegate
                {
                    if (onGraund)
                        rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                });


            ControllerMethods.ButtonDownOneAudio(playerInputs.Attack.key, haveDamage.attack, animator, haveDamage.audioSource, null);

            if (direct != Vector3.zero)
            {
                transform.transform.position += direct;
                if (!haveDamage.audioSource.isPlaying)
                {
                    haveDamage.audioSource.clip = goClip;
                    haveDamage.audioSource.Play();
                }
            }
        }
    }
}