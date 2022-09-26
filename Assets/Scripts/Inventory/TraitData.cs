using System;
using System.Collections.Generic;
using System.Collections;
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
        public Level level;
        public int nextTraitID = -1;
        public bool scalabe = false;
        public List<Stat<MainStatType>> statEffects = new List<Stat<MainStatType>>();
    }

    [Serializable]
    public class Level
    {
        public const int Exp_Per_One = 500;

        public event Action<int> LevelUp;

        [SerializeField] private int _curExp = 0;
        [SerializeField] private int _curLevel = 1;

        [SerializeField] private int _maxExp = 500;
        [SerializeField] private int _maxLevel = 50;

        public int CurExp
        {
            get => _curExp;
            set
            {
                if (CurLevel < MaxLevel)
                    while (value > MaxExp)
                    {
                        value -= MaxExp;
                        CurLevel++;

                        if (CurLevel >= MaxLevel)
                            break;
                    }

                if (value > MaxExp)
                    value = MaxExp;

                if (value < 0)
                    value = 0;


                _curExp = value;

            }
        }
        public int CurLevel
        {
            get => _curLevel;
            private set
            {
                _curLevel = value;

                MaxExp = (int)(MaxExp * 1.1f);

                LevelUp?.Invoke(value);
            }
        }
        public int MaxExp
        {
            get => _maxExp;
            private set
            {
                _maxExp = value;
            }
        }
        public int MaxLevel
        {
            get => _maxLevel;
            set
            {
                _maxLevel = value;
            }
        }

    }
}
