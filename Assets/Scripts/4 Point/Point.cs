using UnityEngine;
using System.Collections.Generic;

public class Point : GraphElement
{
    public List<GameObject> geometric = new List<GameObject>();
    //0 - floor
    //1-4 - walls

    public override void Generate()
    {
        // Create QUAD and give a f*ck
        geometric.Add(FabricGameObject.CreateQuad(transform.position, transform));
        blacklist.Add(this);

        //var aboutPos = transform.position.About();

    }

    public void GenWalls()
    {
        Area area = parentElement.parentElement.parentElement as Area;
        var pullars = new[]
        {
            (Vector3.forward + Vector3.right)*0.5f,
            (Vector3.forward + Vector3.left)*0.5f,
            (Vector3.back + Vector3.right)*0.5f,
            (Vector3.back + Vector3.left)*0.5f,
        };
        var walls = new List<Vector3>() { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
        foreach (var ce in connectElements)
        {
            var way = ce.GetConnect(this).transform.position - transform.position;
            walls.Remove(way);
        }
        foreach (var w in walls)
        {
            geometric.Add(FabricGameObject.InstantiateWallPrefab(area.texturePack.Wall, transform.position, transform, w));
        }

        foreach (var p in pullars)
        {
            var currP = transform.position + p;
            if (!blacklist.ContainsPullar(currP))
            {
                blacklist.AddPullar(currP);
                geometric.Add(FabricGameObject.InstantiatePullarPrefab(area.texturePack.Pullar, currP, transform));
            }
        }
    }


}

