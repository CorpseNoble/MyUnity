using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New Grade Data", menuName = "Grade Data", order = 52)]
    public class GradeData : ScriptableObject
    {
        public List<Grade> Grades = new List<Grade> {
            new Grade { name="Common"},
            new Grade { name="Steel", color = Color.gray},
            new Grade { name="Mithril", color = Color.blue},
            new Grade { name="Orichalcum", color = Color.magenta},
        };
    }

    [Serializable]
    public class Grade
    {
        public string name;
        public string discriptions;
        public Color color = Color.white;
    }
}
