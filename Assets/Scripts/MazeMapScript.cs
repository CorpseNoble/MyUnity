using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MazeMapScript : MonoBehaviour
{
    public Area area;


    public bool Generate = false;
    public bool Reset = false;

    private void OnEnable()
    {
        if (area == null)
            area = gameObject.AddComponent<Area>();

        PosBlacklist ??= new HashSet<Vector3>();
    }
    
    private void OnGUI()
    {
        if (Generate)
        {
            Generate = false;
            area.Generate();
        }
        if (Reset)
        {
            Reset = false;
            foreach(var se in area.subElements)
            {
                
                //Destroy(se.gameObject);
                DestroyImmediate(se.gameObject);
            }
            area.subElements.Clear();
            area.rootElement = null;
            PosBlacklist.Clear();
        }
    }
}
