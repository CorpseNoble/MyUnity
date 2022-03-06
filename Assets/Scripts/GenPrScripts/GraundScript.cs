using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace GenPr1
{
    public class GraundScript : MonoBehaviour
    {
        public bool OnGraund = false;
        public string collisionName = "paladin_prop_j_nordstrom";
        public OnGraundHelper helper;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name != collisionName)
            {
                OnGraund = true;
                helper?.Invoke();
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.name != collisionName && !OnGraund)
            {
                OnGraund = true;
                helper?.Invoke();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.name != collisionName)
            {
                OnGraund = false;
                helper?.Invoke();
            }
        }
    }

    public delegate void OnGraundHelper();
}