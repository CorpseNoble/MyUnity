using UnityEngine;

public class Point : GraphElement
{
    public GameObject[] geometric = new GameObject[5];
    //0 - floor
    //1-4 - walls

    public override void Generate()
    {
        // Create QUAD and give a f*ck
        geometric[0] = FabricGameObject.CreateQuad(transform.position, transform);
        blacklist.Add(transform.position);
        //var aboutPos = transform.position.About();

    }

   
}

