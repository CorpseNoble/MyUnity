using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    [CreateAssetMenu(fileName = "New Graph Settings Data", menuName = "Graph Settings Data", order = 52)]
    public class GraphSettingsData : ScriptableObject
    {
        public int seed = 0;
        public bool pillarAboutWall = true;
        [Range(1, 5)] public int hight = 2;
        [Range(3, 100)] public int maxZoneCount = 60;
        public RandRangeValue nextZoneCount;

        [Header("Way Room Zone")]
        public RandRangeValue waySize;
        public RandRangeValue roomSize;
        public RandRangeValue subzoneSize;

        [Header("Percent")]
        public RandPercentValue noizePercent;
        public RandPercentValue LRPercent;

        [Header("Room")]
        public RandPercentValue wallWindowPercent;
        public RandPercentValue roomPercent;
        public RandPercentValue roundRoomPercent;

        [Header("Room Entry")]
        public RandPercentValue DangerSaveRoomPercent;
        public RandPercentValue EnemyPercent;
        public RandPercentValue TrapPercent;
        public RandPercentValue StairsPercent;

        [HideInInspector] public RandPercentValue corridorPercent;
        [HideInInspector] public RandPercentValue rameZonePercent;
        [HideInInspector] public RandPercentValue nextZonePercent;
        private System.Random random;
        private List<IRandom> randomValues;

        public GraphSettingsData()
        {
            Init();
        }

        private void Init()
        {
            random = new System.Random(seed);


            subzoneSize = new RandRangeValue(5, 2, random);
            roomSize = new RandRangeValue(10, 4, random);
            waySize = new RandRangeValue(5, 2, random);
            noizePercent = new RandPercentValue(1, random);
            LRPercent = new RandPercentValue(5, random);
            nextZoneCount = new RandRangeValue(2, 1, random);
            nextZonePercent = new RandPercentValue(5, random);
            corridorPercent = new RandPercentValue(5, random);
            rameZonePercent = new RandPercentValue(5, random);

            wallWindowPercent = new RandPercentValue(5, random);
            roomPercent = new RandPercentValue(5, random);
            roundRoomPercent = new RandPercentValue(5, random);

            DangerSaveRoomPercent = new RandPercentValue(5, random);
            EnemyPercent = new RandPercentValue(7, random);
            TrapPercent = new RandPercentValue(7, random);
            StairsPercent = new RandPercentValue(5, random);

            randomValues = new List<IRandom>()
            {
            waySize,
            roomSize,
            nextZoneCount,
            noizePercent,
            LRPercent,
            subzoneSize,
            corridorPercent,
            nextZonePercent,
            rameZonePercent,
            roomPercent,
            roundRoomPercent,
            DangerSaveRoomPercent,
            EnemyPercent,
            TrapPercent,
            };
        }
        [ContextMenu("GenerateSeed")]
        public void GenerateSeed()
        {
            seed = new System.Random().Next();
            UpdateSeed();
        }
        [ContextMenu("UpdateSeed")]
        public void UpdateSeed()
        {
            random = new System.Random(seed);
            UpdateRef();
        }
        private void UpdateRef()
        {
            foreach (var rand in randomValues)
            {
                rand.Random = random;
            }
        }

        public IEnumerable<T> SelectRandElements<T>(int sizeSelecion, IEnumerable<T> list)
        {
            var list2 = new List<T>(list);
            var selection = new List<T>();
            if (list2.Count < sizeSelecion)
                return list2;
            for (int i = 0; i < sizeSelecion; i++)
            {
                var x = random.Next(0, list2.Count);
                selection.Add(list2[x]);
                list2.RemoveAt(x);
            }
            return selection;
        }
        public T SelectRandElement<T>(IEnumerable<T> list)
        {
            try
            {
                var list2 = new List<T>(list);
                var x = random.Next(0, list2.Count);
                return list2[x];
            }
            catch
            {
                return list.First();
            }
        }
    }
    [Serializable]
    public class RandRangeValue : IRandom
    {
        [Range(1, 20)]
        public int avarage = 2;
        [Range(0, 10)]
        public int deviation = 1;

        public System.Random Random { get; set; }
        public RandRangeValue(int avarage, int deviation, System.Random random)
        {
            this.avarage = avarage;
            this.deviation = deviation;
            this.Random = random;
        }

        public RandRangeValue(RandRangeValue value, System.Random random)
        {
            this.avarage = value.avarage;
            this.deviation = value.deviation;
            this.Random = random;
        }

        public int MinValue => (int)(avarage - deviation);
        public int MaxValue => (int)(avarage + deviation);

        public int GetValue()
        {
            return Random.Next(MinValue, MaxValue + 1);
        }
    }
    [Serializable]
    public class RandPercentValue : IRandom
    {
        [Range(0, 10)]
        public int percent = 0;

        public System.Random Random { get; set; }
        public RandPercentValue(int percent, System.Random random)
        {
            this.percent = percent;
            this.Random = random;
        }

        public bool GetValue()
        {
            if(percent == 0) return false;
            return Random.Next(0, 11) < (percent + 1);
        }
    }

    public interface IRandom
    {
        public System.Random Random { get; set; }
    }
}
