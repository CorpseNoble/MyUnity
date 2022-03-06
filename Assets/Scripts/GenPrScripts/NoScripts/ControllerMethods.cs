using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace GenPr1
{
    public static class ControllerMethods
    {
        public static bool GetComponentSafe<T>(this GameObject game, out T component) where T : Component
        {
            component = game.GetComponent<T>();
            return component != null;
        }
        public static void SetFloat(KeyCode keyPlus, KeyCode keyMinus, string param, Animator animator)
        {
            if (Input.GetKeyDown(keyPlus))
                animator.SetFloat(param, 1f);
            if (Input.GetKeyUp(keyPlus))
                animator.SetFloat(param, 0f);
            if (Input.GetKeyDown(keyMinus))
                animator.SetFloat(param, -1f);
            if (Input.GetKeyUp(keyMinus))
                animator.SetFloat(param, 0f);
        }

        public static void SetFloat(KeyCode key, string param, Animator animator)
        {
            if (Input.GetKeyDown(key))
                animator.SetFloat(param, 1f);
            if (Input.GetKeyUp(key))
                animator.SetFloat(param, 0f);
        }
        public static void SetBool(KeyCode key, string param, Animator animator)
        {
            if (Input.GetKeyDown(key))
                animator.SetBool(param, true);
            if (Input.GetKeyUp(key))
                animator.SetBool(param, false);
        }
        public static void ButtonDownOne(
            KeyCode key,
            AnimVariable variable,
            Animator animator,
            Action actionDown)
        {
            if (Input.GetKeyDown(key))
            {
                variable.SetVariable(animator, 1);
            }
            if (Input.GetKey(key))
            {
                actionDown?.Invoke();
            }
            if (Input.GetKeyUp(key))
            {
                variable.SetVariable(animator, 0);
            }
        }

        public static void ButtonDownOneAudio(
            KeyCode key,
            AnimVariable variable,
            Animator animator,
            AudioSource source,
            Action actionDown)
        {
            if (Input.GetKeyDown(key))
            {
                variable.SetVariable(animator, 1, source);
            }
            if (Input.GetKey(key))
            {
                actionDown?.Invoke();
            }
            if (Input.GetKeyUp(key))
            {
                variable.SetVariable(animator, 0, source);
            }
        }

        public static void ButtonDownOneOne(
            KeyCode key,
            AnimVariable variable,
            Animator animator,
            Action actionDown)
        {
            if (Input.GetKeyDown(key))
            {
                variable.SetVariable(animator, 1);
                actionDown?.Invoke();
            }
            if (Input.GetKeyUp(key))
            {
                variable.SetVariable(animator, 0);
            }
        }

        public static void ButtonDownTwo(
            KeyCode keyPlus, KeyCode keyMinus,
            AnimVariable variable, Animator animator,
            Action actionDownPlus,
            Action actionDownMinus)
        {
            if (Input.GetKeyDown(keyPlus))
            {
                variable.SetVariable(animator, 1);
            }
            if (Input.GetKey(keyPlus))
            {
                actionDownPlus?.Invoke();
            }
            if (Input.GetKeyUp(keyPlus))
            {
                variable.SetVariable(animator, 0);
            }
            if (Input.GetKeyDown(keyMinus))
            {
                variable.SetVariable(animator, -1);
            }
            if (Input.GetKey(keyMinus))
            {
                actionDownMinus?.Invoke();
            }
            if (Input.GetKeyUp(keyMinus))
            {
                variable.SetVariable(animator, 0);
            }
        }
        public static void SetBool(string param, CharacterController controller, Animator animator, ref bool onGraund)
        {
            if (onGraund != controller.isGrounded)
            {
                onGraund = !onGraund;
                animator.SetBool(param, controller.isGrounded);
            }
        }
        public static void SetBool(string param, GraundScript graund, Animator animator, ref bool onGraund)
        {
            if (onGraund != graund.OnGraund)
            {
                onGraund = !onGraund;
                animator.SetBool(param, onGraund);
            }
        }
        public static void SetTrigger(KeyCode key, string param, Animator animator)
        {
            if (Input.GetKeyDown(key))
                animator.SetTrigger(param);
        }

        public static void SetTrigger(string param, Animator animator)
        {
            animator.SetTrigger(param);
        }
    }

    public class PlayerInputs
    {
        public InputCortage GoFront = new InputCortage() { key = KeyCode.W, AboutText = "Go Front" };
        public InputCortage GoBack = new InputCortage() { key = KeyCode.S, AboutText = "Go Back" };
        public InputCortage GoRight = new InputCortage() { key = KeyCode.D, AboutText = "Go Right" };
        public InputCortage GoLeft = new InputCortage() { key = KeyCode.A, AboutText = "Go Left" };

        public InputCortage RotateRight = new InputCortage() { key = KeyCode.D, AboutText = "Rotate to Right" };
        public InputCortage RotateLeft = new InputCortage() { key = KeyCode.A, AboutText = "Rotate to Left" };

        public InputCortage GoUp = new InputCortage() { key = KeyCode.Space, AboutText = "Go Up" };
        public InputCortage GoDown = new InputCortage() { key = KeyCode.C, AboutText = "Go Down" };
        public InputCortage Run = new InputCortage() { key = KeyCode.LeftShift, AboutText = "Run" };

        public InputCortage Attack = new InputCortage() { key = KeyCode.Mouse0, AboutText = "Attack" };
        public InputCortage Block = new InputCortage() { key = KeyCode.Mouse1, AboutText = "Block" };

        public InputCortage Action = new InputCortage() { key = KeyCode.E, AboutText = "Action" };
        public InputCortage Target = new InputCortage() { key = KeyCode.Tab, AboutText = "Target" };
    }
    public struct InputCortage
    {
        public KeyCode key;
        public string AboutText;
    }
}