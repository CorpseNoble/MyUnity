using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GenPr1
{
    public class MyGravityScript : MonoBehaviour
    {
        public GraundScript graund;

        public float Gravity = 1f;

        private bool onGraund = false;
        private void FixedUpdate()
        {
            if (!onGraund)
            {
                gameObject.transform.Translate(Vector3.down * Gravity * Time.fixedTime);
            }
        }

        private void SetOnGraund()
        {
            onGraund = graund.OnGraund;
        }
    }
}
