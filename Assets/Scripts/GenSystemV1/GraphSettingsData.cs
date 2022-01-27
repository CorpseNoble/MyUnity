using System;
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
        [Range(1,5)] public int hight = 2;
        public RandRangeValue waySize;
        public RandRangeValue roomSize;
        public RandRangeValue pathLenght;
        public RandRangeValue nextZoneCount;
        public RandPercentValue noizePercent;
        public RandPercentValue LRPercent;
        public RandRangeValue subzoneSize;
        [HideInInspector] public RandRangeValue maxZoneCount;

        [Header("Room ZoneType")]
        public RandPercentValue corridorPercent;
        public RandPercentValue nextZonePercent;
        public RandPercentValue rameZonePercent;
        public RandPercentValue roomPercent;
        public RandPercentValue roudRoomPercent;

        [Header("Room Entry")]
        public RandPercentValue DangerSaveRoomPercent;
        public RandPercentValue EnemyPercent;
        public RandPercentValue TrapPercent;

        private System.Random random;
        private List<IRandom> randomValues;

        public GraphSettingsData()
        {
            Init();
        }

        private void Init()
        {
            random = new System.Random(seed);

            maxZoneCount = new RandRangeValue(60, 5, random);

            subzoneSize = new RandRangeValue(5, 2, random);

            roomSize = new RandRangeValue(10, 4, random);
            waySize = new RandRangeValue(5, 2, random);
            pathLenght = new RandRangeValue(5, 2, random);
            noizePercent = new RandPercentValue(1, random);
            LRPercent = new RandPercentValue(5, random);
            nextZoneCount = new RandRangeValue(2, 0.5f, random);
            nextZonePercent = new RandPercentValue(5, random);
            corridorPercent = new RandPercentValue(5, random);
            rameZonePercent = new RandPercentValue(5, random);

            roomPercent = new RandPercentValue(5, random);
            roudRoomPercent = new RandPercentValue(5, random);

            DangerSaveRoomPercent = new RandPercentValue(5, random);
            EnemyPercent = new RandPercentValue(7, random);
            TrapPercent = new RandPercentValue(7, random);

            randomValues = new List<IRandom>()
            {
            waySize,
            roomSize,
            pathLenght,
            nextZoneCount,
            noizePercent,
            LRPercent,
            subzoneSize,
            maxZoneCount,
            corridorPercent,
            nextZonePercent,
            rameZonePercent,
            roomPercent,
            roudRoomPercent,
            DangerSaveRoomPercent,
            EnemyPercent,
            TrapPercent,
            };
        }
        [ContextMenu("GenerateSeed")]
        public void GenerateSeed()
        {
            seed = random.Next();
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

        public IEnumerable<T> SelectRandElements<T>(int sizeSelecion, List<T> list)
        {
            var selection = new List<T>();
            if (list.Count < sizeSelecion)
                return list;

            for (int i = 0; i < sizeSelecion; i++)
            {
                var x = random.Next(0, list.Count);
                selection.Add(list[x]);
                list.RemoveAt(x);
            }
            return selection;
        }
    }
    [Serializable]
    public class RandRangeValue : IRandom
    {
        [Range(1.5f, 20f)]
        public float avarage = 2;
        [Range(0.5f, 10f)]
        public float deviation = 1;

        public System.Random Random { get; set; }
        public RandRangeValue(float avarage, float deviation, System.Random random)
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
            return Random.Next(0, 11) < (percent + 1);
        }
    }

    public interface IRandom
    {
        public System.Random Random { get; set; }
    }
}
