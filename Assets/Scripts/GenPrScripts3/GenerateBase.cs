using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace GenPr3
{
    [ExecuteInEditMode]
    public abstract class GenerateBase : MonoBehaviour
    {
        public PropertyButton StartGenerate;
        public abstract NodeMap Generate(NodeMap nodeMap);

        public NodeMap nodeMap;

        protected virtual void Install()
        {
            StartGenerate = new PropertyButton(() => Generate(nodeMap));
        }
        protected virtual void Awake()
        {
            Install();
        }
    }


}
