using UnityEngine;

[ExecuteInEditMode]
public class FabricGameObject
{
    public static T InstantiateElement<T>(Vector3 position, GraphElement parent, Vector3 buildVector) where T : GraphElement
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = position;
        gameObject.transform.parent = parent.transform;
        gameObject.name = typeof(T).Name;
        var t = gameObject.AddComponent<T>();
        t.blacklist = parent.blacklist;
        t.buildVector = buildVector;
        t.parentElement = parent;
        return t;
    }
    public static T Instantiate<T>(Vector3 position, Transform parent) where T : Component
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = position;
        gameObject.transform.parent = parent;
        return gameObject.AddComponent<T>();
    }

    public static GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Transform parent)
    {
        return Object.Instantiate(prefab, position, Quaternion.identity, parent);
    }
    public static GameObject CreateQuad(Vector3 position, Transform parent)
    {
        var quad2 = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad2.transform.forward = Vector3.up;
        quad2.transform.position = position + Vector3.up;
        quad2.transform.parent = parent;

        var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.forward = Vector3.down;
        quad.transform.position = position;
        quad.transform.parent = parent;
        return quad;

      
    }
    public static GameObject CreateQuadWall(Vector3 position, Transform parent,Vector3 vector)
    {
        var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.forward = vector;
        quad.transform.position = position + vector * 0.5f + Vector3.up * 0.5f;
        quad.transform.parent = parent;
        return quad;
    }

    public static GameObject InstantiateWallPrefab(GameObject wall,Vector3 position, Transform parent, Vector3 vector)
    {
        var gameW = GameObject.Instantiate(wall);
        gameW.transform.forward = vector;
        gameW.transform.position = position + vector * 0.5f + Vector3.up * 0.5f;
        gameW.transform.parent = parent;
        return gameW;
    }

    public static GameObject InstantiatePullarPrefab(GameObject Pullar, Vector3 position, Transform parent)
    {
        var gameW = GameObject.Instantiate(Pullar);
        gameW.transform.position = position + Vector3.up * 0.5f;
        gameW.transform.parent = parent;
        return gameW;
    }
}

