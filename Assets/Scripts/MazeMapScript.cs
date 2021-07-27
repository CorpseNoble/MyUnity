using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMapScript : MonoBehaviour
{
    public Zone zone;

    public bool Generate = false;
    public bool Reset = false;

    private void OnEnable()
    {
        if (zone == null)
            zone = gameObject.AddComponent<Zone>();
    }
    
    private void OnGUI()
    {
        if (Generate)
        {
            Generate = false;
            zone.Generate();
        }
        if (Reset)
        {
            Reset = false;
            foreach(var se in zone.subElements)
                Destroy(se.gameObject);
            zone.subElements.Clear();
            zone.rootElement = null;
        }
    }
}
