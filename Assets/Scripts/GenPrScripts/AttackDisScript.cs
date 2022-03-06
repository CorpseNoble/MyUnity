using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace GenPr1
{
    public class AttackDisScript : MonoBehaviour
    {
        public Collider TriggerAttack;
        public void CollDisEnabled()
        {
            TriggerAttack.enabled = false;
        }
        public void CollEnabled()
        {
            TriggerAttack.enabled = true;
        }

        private void Start()
        {
            CollDisEnabled();
        }
    }
}
