using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class GraphElement : MonoBehaviour
{
    //internal struct
    public GraphElement rootElement;
    public List<GraphElement> subElements = new List<GraphElement>();
    public List<(GraphElement elem, Vector3 pos, Vector3 forw)> newWays = new List<(GraphElement elem, Vector3 pos, Vector3 forw)>();

    //external struct
    public Vector3 buildVector = Vector3.forward;
    public GraphElement parentElement;
    public List<Connect> connectElements = new List<Connect>();
    public GraphElement backElement;
    /// <summary>
    /// Create internal struct
    /// </summary>
    public abstract void Generate();

    public void Connect(GraphElement graph)
    {
        var connect = new Connect(this, graph);

        if (connectElements.Contains(connect))
            return;

        connectElements.Add(connect);
        graph.connectElements.Add(connect);

        if(parentElement != graph.parentElement)
        {
            parentElement.Connect(graph.parentElement);
        }
    }
}

