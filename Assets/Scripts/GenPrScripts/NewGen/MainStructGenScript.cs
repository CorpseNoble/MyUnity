using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace GenPr1
{
    [ExecuteInEditMode]
    public class MainStructGenScript : MonoBehaviour
    {
        //public Interect GenInEditorMode;
        //public Interect DestroyInEditorMode;
        //public Interect ViewDebug;

        public GenNodeMapBase genNodeMap;
        public GenWallBase genWall;

        private NodeMap nodeMap;
        //private void OnEnable()
        //{
        //    GenInEditorMode = new Interect(Gen);
        //    DestroyInEditorMode = new Interect(ClearAll);
        //    ViewDebug = new Interect(DebugRender);
        //}
        [ContextMenu("Gen")]
        public void Gen()
        {
            ClearAll();

            nodeMap = genNodeMap.GenerateNodeMap();

            genWall.GenWay(nodeMap);
        }
        [ContextMenu("ClearAll")]

        public void ClearAll()
        {
            //foreach (var b in nodeMap.nodes)
            //    DestroyImmediate(b.gameObject);

            var childCount = gameObject.transform.childCount;

            for (int i = 0; i < childCount; i++)
                DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
        }
        [ContextMenu("DebugRender")]

        public void DebugRender()
        {
            foreach (var n in nodeMap.nodes)
                foreach (var n2 in n.sides)
                    Debug.DrawLine(n.transform.position, n2.Value.n.transform.position, Color.white, 10f);
        }
    }
}