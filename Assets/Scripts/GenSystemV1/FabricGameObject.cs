using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.GenSystemV1
{
    [ExecuteInEditMode]
    public static class FabricGameObject
    {
        public static PointElementsData elementsData => PrefsGraph.Instant.elementsData;


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
            t.hight = parent.hight;
            return t;
        }
        public static T Instantiate<T>(Vector3 position, Transform parent) where T : Component
        {
            GameObject gameObject = new GameObject();
            gameObject.transform.position = position;
            gameObject.transform.parent = parent;
            return gameObject.AddComponent<T>();
        }

        public static GameObject InstantiatePrefabPoint(GameObject prefab, Point point)
        {
            point.pleced = true;
            return GameObject.Instantiate(prefab, point.transform.position, Quaternion.identity, point.transform);
        }

        public static GameObject InstGro(Vector3 position, Transform parent) =>
             InstantiateVectoredPrefab(elementsData.Ground, position, parent, Vector3.up);
        public static GameObject InstRoof(Vector3 position, Transform parent) =>
            InstantiateVectoredPrefab(elementsData.Roof, position, parent, Vector3.up);
        public static List<GameObject> InstWallH(Vector3 position, Transform parent, Vector3 vector, int hight) =>
             InstantiateHightPrefab(elementsData.Wall, position, parent, -vector, hight);
        private static GameObject InstWall(Vector3 position, Transform parent, Vector3 vector) =>
            InstantiateVectoredPrefab(elementsData.Wall, position, parent, -vector);
        public static GameObject InstLight(Vector3 position, Transform parent, Vector3 vector) =>
            InstantiateVectoredPrefab(elementsData.Light, position, parent, vector);
        private static List<GameObject> InstDoubleWall(Vector3 position, Transform parent, Vector3 vector) =>
            new List<GameObject>()
            {
                InstantiateVectoredPrefab(elementsData.Wall, position, parent, vector),
                InstantiateVectoredPrefab(elementsData.Wall, position, parent, -vector),
            };
        public static GameObject InstPath(Vector3 position, Transform parent, Vector3 vector) =>
             InstantiateVectoredPrefab(elementsData.GroundPathWay, position, parent, -vector);
        public static List<GameObject> InstDoorH(Vector3 position, Transform parent, Vector3 vector, int hight) =>
            InstantiateHightPrefab(elementsData.Door, position, parent, -vector, hight, true);
        public static List<GameObject> InstLatticeH(Vector3 position, Transform parent, Vector3 vector, int hight) =>
          InstantiateHightPrefab(elementsData.Lattice, position, parent, -vector, hight, true);

        public static GameObject InstGrPillar(Vector3 position, Transform parent) =>
             InstantiateVectoredPrefab(elementsData.GroundPillar, position, parent);
        private static List<GameObject> InstantiateHightPrefab(
            GameObject prefab,
            Vector3 position,
            Transform parent,
            Vector3 vector,
            int hight,
            bool dual = false)
        {
            var gameobjects = new List<GameObject>();
            for (int i = 0; i < hight; i++)
            {
                if (i == 0)
                    gameobjects.Add(InstantiateVectoredPrefab(prefab, position, parent, vector));
                else if (dual)
                    gameobjects.AddRange(InstDoubleWall(position.StepV(i), parent, vector));
                else
                    gameobjects.Add(InstWall(position.StepV(i), parent, -vector));
            }
            return gameobjects;
        }
        private static GameObject InstPillorBase1(Vector3 position, Transform parent) =>
           InstantiateVectoredPrefab(elementsData.PillarBase1, position, parent);
        private static GameObject InstPillorBase2(Vector3 position, Transform parent) =>
           InstantiateVectoredPrefab(elementsData.PillarBase2, position, parent);
        private static GameObject InstPillorUp(Vector3 position, Transform parent) =>
          InstantiateVectoredPrefab(elementsData.PillarUp, position, parent);
        private static GameObject InstPillorDown(Vector3 position, Transform parent) =>
         InstantiateVectoredPrefab(elementsData.PillarDown, position, parent);
        public static List<GameObject> InstHiPillar(Vector3 position, Transform parent, int hight)
        {
            var gameobjects = new List<GameObject>();
            for (int i = 0; i < hight; i++)
            {
                if (i == 0)
                    InstPillorDown(position, parent);
                if (i == hight - 1)
                    InstPillorUp(position.StepV(i), parent);

                InstPillorBase1(position.StepV(i), parent);
                if (i < hight - 1)
                    InstPillorBase2(position.StepV(i), parent);
            }
            return gameobjects;
        }

        private static GameObject InstantiateVectoredPrefab(
            GameObject prefab,
            Vector3 position,
            Transform parent,
            Vector3? vector = null)
        {
            var gameW = GameObject.Instantiate(prefab);
            if (vector.HasValue)
                gameW.transform.forward = vector.Value;
            gameW.transform.position = position;
            gameW.transform.parent = parent;
            return gameW;
        }


        #region Extentions
        public static Vector3 StepH(this Vector3 pos, Vector3 vector, float distance = 1)
        {
            return pos + vector * elementsData.HorScale * distance;
        }
        public static Vector3 StepV(this Vector3 pos, float distance = 1)
        {
            return pos + Vector3.up * elementsData.VerScale * distance;
        }

        public static Vector3 ToRight(this Vector3 vector)
        {
            if (vector == Vector3.forward) return Vector3.right;
            else if (vector == Vector3.right) return Vector3.back;
            else if (vector == Vector3.back) return Vector3.left;
            else if (vector == Vector3.left) return Vector3.forward;
            else return Vector3.forward;
        }
        /// <summary>
        /// set value vector
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 ToTheRight(this Vector3 vector)
        {
            vector = vector.ToRight();
            return vector;
        }
        public static Vector3 ToLeft(this Vector3 vector)
        {
            if (vector == Vector3.forward) return Vector3.left;
            else if (vector == Vector3.left) return Vector3.back;
            else if (vector == Vector3.back) return Vector3.right;
            else if (vector == Vector3.right) return Vector3.forward;
            else return Vector3.forward;
        }
        /// <summary>
        /// set value vector
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 ToTheLeft(this Vector3 vector)
        {
            vector = vector.ToLeft();
            return vector;
        }
        public static Vector3[] About(this Vector3 position)
        {
            return new[] { position.StepH(Vector3.forward), position.StepH(Vector3.left), position.StepH(Vector3.back), position.StepH(Vector3.right) };
        }

        #endregion
    }
}
