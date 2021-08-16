using UnityEngine;

[ExecuteInEditMode]
public class FabricGameObject
{
    public static T InstantiateElement<T>(Vector3 position, GraphElement parent,Vector3 buildVector) where T: GraphElement
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
        var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.forward = Vector3.down;
        quad.transform.position = position;
        quad.transform.parent = parent;
        return quad;
    }
}

