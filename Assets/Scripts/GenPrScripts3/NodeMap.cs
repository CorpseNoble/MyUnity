using System;
using System.Collections.Generic;

namespace GenPr3
{

[Serializable]
public class NodeMap
{
    public IEnumerable<Node> eNodes;
    public Node rootNode;

    public NodeMap(Node root)
    {
        rootNode = root;
        eNodes = new List<Node>();
    }
    private List<Node> Nodes => eNodes as List<Node>;

    /// <summary>
    /// add Node to node map
    /// </summary>
    /// <param name="nodeFrom"> 
    /// add bind to this node bind list 
    /// </param>
    /// <param name="nodeTo">
    /// node in NodeBind
    /// </param>
    public void AddNode(Node nodeFrom, Node nodeTo, BindData data)
    {
        nodeFrom.nodeBinds.Add(new NodeBind(nodeTo, data));

        if (!Nodes.Contains(nodeTo))
            Nodes.Add(nodeTo);
    }

    public void AttachNodeMap(Node nodeFrom, NodeMap nodeMap, BindData data)
    {
        Nodes.AddRange(nodeMap.eNodes);

        nodeFrom.nodeBinds.Add(new NodeBind(nodeMap.rootNode, data));
    }
}

[Serializable]
public class Node
{
    public List<NodeBind> nodeBinds = new List<NodeBind>();
}

[Serializable]
public class NodeBind
{
    public Node node;
    public BindData data;

    public NodeBind(Node node, BindData data)
    {
        this.node = node;
        this.data = data;
    }
}

[Serializable]
public class BindData
{

}

}
