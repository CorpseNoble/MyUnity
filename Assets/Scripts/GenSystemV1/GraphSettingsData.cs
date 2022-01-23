﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    [CreateAssetMenu(fileName = "New Graph Settings Data", menuName = "Graph Settings Data", order = 52)]
    public class GraphSettingsData : ScriptableObject
    {
        public int seed = 0;
        public bool PillarAboutWall = false;
        public RandRangeValue waySize;
        public RandRangeValue roomSize;
        public RandRangeValue pathLenght;
        public RandRangeValue subzoneSize;
        public RandRangeValue maxZoneCount;
        public RandRangeValue nextZoneCount;
        public RandPercentValue noizePercent;
        public RandPercentValue LRPercent;
        public RandPercentValue corridorPercent;
        public RandPercentValue nextZonePercent;
        public RandPercentValue rameZonePercent;
        public RandPercentValue roomPercent;
        public RandPercentValue roudRoomPercent;

        public System.Random random;
        
      
        public GraphSettingsData()
        {
            UpdateSeed();
            GenerateSeed();
            maxZoneCount = new RandRangeValue(21, 5, random);
            subzoneSize = new RandRangeValue(5, 2, random);
            roomSize = new RandRangeValue(4, 1, random);
            waySize = new RandRangeValue(1.5f, 0.5f, random);
            pathLenght = new RandRangeValue(5, 2, random);
            noizePercent = new RandPercentValue(1, random);
            LRPercent = new RandPercentValue(5, random);
            nextZoneCount = new RandRangeValue(2, 0.5f, random);
            nextZonePercent = new RandPercentValue(5, random);
            corridorPercent = new RandPercentValue(5, random);
            rameZonePercent = new RandPercentValue(5, random);
            roomPercent = new RandPercentValue(5, random);
            roudRoomPercent = new RandPercentValue(5, random);
        }
        [ContextMenu("GenerateSeed")]
        public void GenerateSeed()
        {
            seed = random.Next();
            UpdateSeed();
        }

        public void UpdateSeed()
        {
            random = new System.Random(seed);
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
    public class RandRangeValue
    {
        [Range(1.5f, 20f)]
        public float avarage = 2;
        [Range(0.5f, 10f)]
        public float deviation = 1;

        public System.Random random;
        public RandRangeValue(float avarage, float deviation, System.Random random)
        {
            this.avarage = avarage;
            this.deviation = deviation;
            this.random = random;
        }

        public int MinValue => (int)(avarage - deviation);
        public int MaxValue => (int)(avarage + deviation);

        public int GetValue()
        {
            return random.Next(MinValue, MaxValue + 1);
        }
    }
    [Serializable]
    public class RandPercentValue
    {
        [Range(0, 10)]
        public int percent = 0;


        public System.Random random;
        public RandPercentValue(int percent, System.Random random)
        {
            this.percent = percent;
            this.random = random;
        }

        public bool GetValue()
        {
            return random.Next(0, 11) < (percent + 1);
        }
    }
}