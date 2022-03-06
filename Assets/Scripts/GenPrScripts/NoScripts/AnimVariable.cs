using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;


namespace GenPr1
{
    [Serializable]
    public class AnimVariable
    {
        public string nameVariable;
        public AnimatorControllerParameterType type;
        public AudioClip audio;

        public AnimVariable(string nameVariable, AnimatorControllerParameterType type)
        {
            this.nameVariable = nameVariable;
            this.type = type;
        }

        public void SetVariable(Animator animator, float value = 0f, AudioSource source = null)
        {
            switch (type)
            {
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(nameVariable, value > 0);
                    break;
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(nameVariable, value);
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(nameVariable, (int)value);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animator.SetTrigger(nameVariable);
                    break;
            }
            if (audio != null)
                source?.PlayOneShot(audio);
        }
    }


    [Serializable]
    public class TargetMask
    {
        public MeshRenderer MeshRenderer;

        public Material NormalMaterial;
        public Material CanMaterial;
        public Material TargetedMaterial;

        public GameObject target;
    }
}