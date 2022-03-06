using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New Trait Data", menuName = "Trait Data", order = 52)]
    public class TraitData : ScriptableObject
    {
        public List<CategoryTraitData> categoryData;
    }
    [Serializable]
    public class Trait : NameDisciption
    {
        //Quantity exp per one trait using
        public const int Exp_Per_One = 10;

        public int expirience = 0;
        public int nextTraitID = -1;
        public bool scalabe = false;
        public List<Stat<MainStatType>> statEffects = new List<Stat<MainStatType>>();
    }
}
