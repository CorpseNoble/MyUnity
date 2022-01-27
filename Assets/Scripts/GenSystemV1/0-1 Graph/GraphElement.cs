using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GenSystemV1
{
    public abstract class GraphElement : MonoBehaviour
    {
        [Header("GraphElement")]
        //gen string
        public BlacklistManager blacklist;

        //internal struct
        public GraphElement rootElement;
        public List<GraphElement> subElements = new List<GraphElement>();
        public List<(GraphElement elem, Vector3 pos, Vector3 forw)> newWays = new List<(GraphElement elem, Vector3 pos, Vector3 forw)>();

        //external struct
        public Vector3 buildVector = Vector3.forward;
        public GraphElement parentElement;
        public List<Connect> connectElements = new List<Connect>();
        public GraphElement backElement;
       
        public int hight = 1;

        public float HScale { get => hScale; set => hScale = value; }
        public float VScale { get => vScale; set => vScale = value; }

        [SerializeField] private float hScale = 1;
        [SerializeField] private float vScale = 1;
        /// <summary>
        /// Create internal struct
        /// </summary>
        public abstract void Generate();

        public Connect Connect(GraphElement graph)
        {
            var connect = new Connect(this, graph);

            if (connectElements.Contains(connect))
                return connect;

            connectElements.Add(connect);
            graph.connectElements.Add(connect);

            if (parentElement != graph.parentElement)
            {
                var parCon = parentElement.Connect(graph.parentElement);
                parCon.lowConnect = connect;
            }
            return connect;
        }
    }

}
