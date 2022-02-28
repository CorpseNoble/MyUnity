using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New CategoryTrait Data", menuName = "CategoryTrait Data", order = 52)]
    public class CategoryTraitData : ScriptableObject
    {
        public List<Trait> traits = new List<Trait>();
    }
}
