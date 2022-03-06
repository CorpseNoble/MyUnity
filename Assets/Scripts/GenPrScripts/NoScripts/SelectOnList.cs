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
    public class SelectOnList
    {
        [SerializeField]
        public IEnumerable<MonoBehaviour> list;
        public MonoBehaviour Value { get => (index >= 0 && index < list.Count() ? list.ElementAt(index) : null); }
        [SerializeField]
        public int index = -1;
        public SelectOnList(IEnumerable<MonoBehaviour> list)
        {
            this.list = list;
        }
    }
}
