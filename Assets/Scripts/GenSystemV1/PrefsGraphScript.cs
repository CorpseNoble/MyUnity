using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
namespace Assets.Scripts.GenSystemV1
{
    [ExecuteInEditMode]
    public class PrefsGraphScript : MonoBehaviour
    {
        public PrefsGraph prefsGraph = PrefsGraph.Instant;
    }

    [Serializable]
    public class PrefsGraph
    {
        public GraphSettingsData SettingGraph;
        public PointElementsData elementsData;

        public static PrefsGraph Instant
        {
            get
            {
                if (instant == null)
                    instant = new PrefsGraph();

                return instant;
            }
        }
        private static PrefsGraph instant;

        private PrefsGraph() { }
    }

}
