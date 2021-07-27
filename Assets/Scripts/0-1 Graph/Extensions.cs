using UnityEngine;

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

    public static Vector3 ToLeft(this Vector3 vector)
    {
        if (vector == Vector3.forward) return Vector3.left;
        else if (vector == Vector3.left) return Vector3.back;
        else if (vector == Vector3.back) return Vector3.right;
        else if (vector == Vector3.right) return Vector3.forward;
        else return Vector3.forward;
    }

    public static Vector3[] About(this Vector3 position)
    {
        return new[] { position + Vector3.forward, position + Vector3.left, position + Vector3.back, position + Vector3.right };
    }
}

