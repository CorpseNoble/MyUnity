﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


namespace GenPr1
{
    [ExecuteInEditMode]
    public abstract class GenerateBase : MonoBehaviour
    {
        public Action Complite;

        public Interect Start;

        private void OnEnable()
        {
            Start = new Interect(StartGenerate);
        }
        public virtual void StartGenerate()
        {
            Complite?.Invoke();
        }
    }
}
