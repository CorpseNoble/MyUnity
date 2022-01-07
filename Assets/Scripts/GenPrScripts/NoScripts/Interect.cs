using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

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

