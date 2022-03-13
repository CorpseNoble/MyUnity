using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [Serializable]
    public class Status
    {
        public PlayerStats<BarStatType> this[BarStatType type] => playerBarStats.Where(c => c.stat == type).First();
        public PlayerStats<MainStatType> this[MainStatType type] => playerMainStats.Where(c => c.stat == type).First();
        public PlayerStats<SecStatType> this[SecStatType type] => playerSecStats.Where(c => c.stat == type).First();



        [SerializeField] public List<PlayerStats<MainStatType>> playerMainStats;
        [SerializeField] public List<PlayerStats<SecStatType>> playerSecStats;
        [SerializeField] public List<PlayerStats<BarStatType>> playerBarStats;

        public PlayerTrait raceTrait;

        public event Action<Status> StatusChanged;
        public event Action<BarStatType, int> BarStatChanged;
        public event Action<MainStatType, int> MainStatChanged;
        public event Action<SecStatType, int> SecStatChanged;

        [SerializeField, Range(1, 9)] private int _lowEff = 1;
        [SerializeField, Range(1, 9)] private int _medEff = 3;
        [SerializeField, Range(1, 9)] private int _higEff = 5;
        public Status()
        {
            FillStat(out playerMainStats);
            FillStat(out playerSecStats);
            FillStat(out playerBarStats);
        }
        public void OnStatusChanged()
        {
            StatusChanged?.Invoke(this);
        }
        public void OnStatChanged<T>(T stat, int value) where T : struct
        {
            if (stat is BarStatType bar)
                OnBarStatChanged(bar, value);
            if (stat is MainStatType main)
                OnMainStatChanged(main, value);
            if (stat is SecStatType sec)
                OnSecStatChanged(sec, value);
        }
        public void OnBarStatChanged(BarStatType barStat, int value)
        {
            BarStatChanged?.Invoke(barStat, value);
        }
        public void OnMainStatChanged(MainStatType mainStat, int value)
        {
            MainStatChanged?.Invoke(mainStat, value);
        }
        public void OnSecStatChanged(SecStatType secStat, int value)
        {
            SecStatChanged?.Invoke(secStat, value);
        }
        private void ApplyRace()
        {
            foreach (var eff in raceTrait.Trait.statEffects)
                UpMainStat(eff.tType, eff.value);
        }
        public void Clear()
        {
            playerBarStats.Clear();
            playerMainStats.Clear();
            playerSecStats.Clear();
            FillStat(out playerMainStats);
            FillStat(out playerSecStats);
            FillStat(out playerBarStats);
            ApplyRace();

        }
        public void FillStat<T>(out List<PlayerStats<T>> playerStats) where T : struct
        {
            playerStats = new List<PlayerStats<T>>();
            var stats = typeof(T).GetEnumValues();

            foreach (T stat in stats)
            {
                var playerstat = new PlayerStats<T>() { stat = stat };
                playerstat.ValueChanged += OnStatChanged;
                playerStats.Add(playerstat);
            }
        }

        public void UpMainStat(MainStatType mainStat, int value)
        {
            GetStat(mainStat, playerMainStats).Value += value;

            switch (mainStat)
            {
                case MainStatType.Strength:

                    GetStat(SecStatType.PhAttack, playerSecStats).Value += value * _higEff;
                    GetStat(SecStatType.Weight, playerSecStats).Value += value * _medEff;

                    GetStat(BarStatType.HP, playerBarStats).Value += value * _lowEff;
                    GetStat(BarStatType.FP, playerBarStats).Value += value * _lowEff;

                    break;
                case MainStatType.Agility:

                    GetStat(SecStatType.Accuracity, playerSecStats).Value += value * _medEff;
                    GetStat(SecStatType.CritRate, playerSecStats).Value += value * _lowEff;
                    GetStat(SecStatType.Avade, playerSecStats).Value += value * _medEff;

                    GetStat(BarStatType.HP, playerBarStats).Value += value * _lowEff;
                    GetStat(BarStatType.FP, playerBarStats).Value += value * _lowEff;

                    break;
                case MainStatType.Vitality:

                    GetStat(SecStatType.PhRes, playerSecStats).Value += value * _higEff;
                    GetStat(SecStatType.MagRes, playerSecStats).Value += value * _lowEff;
                    GetStat(SecStatType.Weight, playerSecStats).Value += value * _lowEff;

                    GetStat(BarStatType.HP, playerBarStats).Value += value * _higEff;
                    GetStat(BarStatType.FP, playerBarStats).Value += value * _medEff;
                    GetStat(BarStatType.Fatigue, playerBarStats).Value += value * _medEff;

                    break;
                case MainStatType.Endurance:

                    GetStat(SecStatType.PhRes, playerSecStats).Value += value * _lowEff;
                    GetStat(SecStatType.MagRes, playerSecStats).Value += value * _higEff;
                    GetStat(SecStatType.Weight, playerSecStats).Value += value * _lowEff;

                    GetStat(BarStatType.HP, playerBarStats).Value += value * _medEff;
                    GetStat(BarStatType.FP, playerBarStats).Value += value * _higEff;
                    GetStat(BarStatType.Fatigue, playerBarStats).Value += value * _medEff;

                    break;
                case MainStatType.Intelligence:

                    GetStat(SecStatType.MagAttack, playerSecStats).Value += value * _higEff;

                    GetStat(BarStatType.MP, playerBarStats).Value += value * _lowEff;
                    GetStat(BarStatType.Stress, playerBarStats).Value += value * _higEff;

                    break;
                case MainStatType.Knowledge:

                    GetStat(SecStatType.Accuracity, playerSecStats).Value += value * _lowEff;
                    GetStat(SecStatType.CritRate, playerSecStats).Value += value * _medEff;

                    GetStat(BarStatType.MP, playerBarStats).Value += value * _higEff;
                    GetStat(BarStatType.Stress, playerBarStats).Value += value * _lowEff;

                    break;
                case MainStatType.Luck:

                    GetStat(SecStatType.DropRate, playerSecStats).Value += value * _medEff;
                    GetStat(SecStatType.Accuracity, playerSecStats).Value += value * _lowEff;
                    GetStat(SecStatType.CritRate, playerSecStats).Value += value * _lowEff;

                    break;

            }
        }
        public PlayerStats<T> GetStat<T>(T stat, List<PlayerStats<T>> stats) where T : struct
        {
            return stats.Where(c => c.stat.Equals(stat)).First();
        }

        [Serializable]
        public class PlayerStats<T> where T : struct
        {
            public event Action<T, int> ValueChanged;
            public T stat;
            [SerializeField] private int _value;

            public int Value
            {
                get => _value;
                set
                {
                    this._value = value;
                    ValueChanged?.Invoke(stat, value);
                }
            }
        }

    }

}
