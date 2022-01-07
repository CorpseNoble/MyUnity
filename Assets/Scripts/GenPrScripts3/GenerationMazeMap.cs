using UnityEngine;
using System.Collections;
namespace GenPr3
{
    public class GenerationMazeMap : GenerateBase
{
    [Range(1,5)]
    public int pathWidth = 1;
    [Range(1,20)]
    public int pathLength = 1;
    public override NodeMap Generate(NodeMap nodeMap)
    {
        throw new System.NotImplementedException();
    }
}
}
