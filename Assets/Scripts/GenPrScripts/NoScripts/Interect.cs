using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace GenPr1
{

    [Serializable]
    public class Interect
    {
        //public bool button;

        public Action action;

        //public void Check()
        //{
        //    if (button)
        //    {
        //        action?.Invoke();
        //        button = false;
        //    }
        //}

        public Interect(Action action)
        {
            this.action = action;
        }
    }
}

