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
        public List<Trait> Traits = new List<Trait>();
    }
    [Serializable]
    public class Trait
    {
        public string Name = "";
        public string Descriptions = "Trait Description";
        public int expirience = 0;
        public int NextTraitID = -1;
        public bool Scalabe = false;
        public List<Stat> Effects = new List<Stat>();
    }
}
