using UnityEngine;
namespace Assets.Scripts.GenSystemV1
{
    public static class Extensions
    {
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
        public static Vector3[] About(this Vector3 position, float scale = 1)
        {
            return new[] { position + Vector3.forward * scale, position + Vector3.left * scale, position + Vector3.back * scale, position + Vector3.right * scale };
        }
    }

}
