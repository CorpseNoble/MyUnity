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
            new Grade { Name="Common"},
            new Grade { Name="Steel", color = Color.gray},
            new Grade { Name="Mithril", color = Color.blue},
            new Grade { Name="Orichalcum", color = Color.magenta},
        };
    }

    [Serializable]
    public class Grade
    {
        public string Name;
        public string Discriptions;
        public Color color = Color.white;
    }
}
