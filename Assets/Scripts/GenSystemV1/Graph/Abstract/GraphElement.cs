using System;
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
        public GraphElement backElementLow;

        //internal struct
        public GraphElement rootElement;
        public List<GraphElement> subElements = new List<GraphElement>();
        public List<NewWay> newWays = new List<NewWay>();
        public int number = 0;

        //external struct
        public Vector3 buildVector = Vector3.forward;
        public GraphElement parentElement;
        public List<Connect> connectElements = new List<Connect>();
        public GraphElement backElement;
       
        public int hight = 1;

        /// <summary>
        /// Create internal struct
        /// </summary>
        public abstract void Generate();

        public Connect Connect(GraphElement graph, ConnectType connectType = ConnectType.Path)
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
                connect.highConnect = parCon;
            }
            connect.connectType = connectType;
            return connect;
        }
    }
    [Serializable]
    public struct NewWay
    {
        public GraphElement elemement;
        public Vector3 position;
        public Vector3 vector;
        public NewWay(GraphElement elemement, Vector3 position, Vector3 vector) : this()
        {
            this.elemement = elemement;
            this.position = position;
            this.vector = vector;
        }
        public void Deconstruct(out GraphElement elem, out Vector3 pos,out Vector3 forw)
        {
            elem = elemement;
            pos = position;
            forw = vector;
        }
    }
}
