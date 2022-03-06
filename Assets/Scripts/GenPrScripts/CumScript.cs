using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GenPr1
{
    public class CumScript : MonoBehaviour
    {
        public GameObject target;

        [Range(1f, 10f)]
        public float distance = 1;

        //public Transform pos;

        private void Start()
        {
            //transform.position = pos.position;

            //transform.rotation = pos.rotation;

        }

        private void Update()
        {
            //transform.position = pos.position;
            CumNormalized();
        }

        public void CumNormalized()
        {
            transform.forward = (target.transform.position - transform.position).normalized;
        }
    }
}