using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GenPr3
{
    public class GenerateMainScript : GenerateBase
{
    public List<GenerateBase> generates = new List<GenerateBase>();

    public override NodeMap Generate(NodeMap nodeMap)
    {
        foreach (var gen in generates)
            nodeMap = gen.Generate(nodeMap);

        return nodeMap;
    }
}
}
