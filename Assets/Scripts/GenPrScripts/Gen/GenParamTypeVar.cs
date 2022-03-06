using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace GenPr1
{
    [Serializable]
    public abstract class GenParamTypeVar
    {
        public RoomSide side;
        public string name;
        [Range(0, max)]
        public int weight;

        public GenParamTypeVar(RoomSide side)
        {
            this.side = side;
        }

        private static AllSide allSide = new AllSide();
        private const int max = 20;


    }
}
