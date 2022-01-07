using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MazeMapScript : MonoBehaviour
{
    public Area area;

    public BlacklistManager blacklist;
    public SettingGraphScript settingGraph;
    
    private void OnEnable()
    {
        if (area == null)
            area = gameObject.AddComponent<Area>();

        blacklist ??= new BlacklistManager();

    }
    private void GenStepWall()
    {

        var points = new List<GraphElement>();
        foreach (var seZ in area.subElements)
        {
            foreach (var seR in seZ.subElements)
            {
                points.AddRange(seR.subElements);

            }
        }
        foreach (Point p in points)
        {
            p.GenWalls();
        }
    }
    [ContextMenu("Gen")]
    public void Gen()
    {
        if (area.subElements.Count>0)
        {
            Clear();
        }

        settingGraph.SettingGraph.UpdateSeed();
        area.blacklist = blacklist;
        area.Generate();
        GenStepWall();
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        foreach (var se in area.subElements)
        {

            //Destroy(se.gameObject);
            DestroyImmediate(se.gameObject);
        }
        area.subElements.Clear();
        area.rootElement = null;
        blacklist.Clear();
    }

}
