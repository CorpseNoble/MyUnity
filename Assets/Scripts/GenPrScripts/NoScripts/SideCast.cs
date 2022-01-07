using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public static class SideCast
{
    public static List<SideType> GetForBack
    { get { return new List<SideType>() { SideType.Forward, SideType.Back }; } }
    public static List<SideType> GetLeftRight
    { get { return new List<SideType>() { SideType.Left, SideType.Right }; } }
    public static List<SideType> GetUpDown
    { get { return new List<SideType>() { SideType.Up, SideType.Down }; } }

    public static Vector3 CastSideTypeToVector3(SideType type)
    {
        switch (type)
        {
            case SideType.Back:
                return Vector3.back;
            case SideType.Forward:
                return Vector3.forward;
            case SideType.Left:
                return Vector3.left;
            case SideType.Right:
                return Vector3.right;
            case SideType.Up:
                return Vector3.up / 2;
            case SideType.Down:
                return Vector3.down / 2;
        }
        return Vector3.zero;
    }
    public static SideType RevertSideType(SideType type)
    {
        switch (type)
        {
            case SideType.Back:
                return SideType.Forward;
            case SideType.Forward:
                return SideType.Back;
            case SideType.Left:
                return SideType.Right;
            case SideType.Right:
                return SideType.Left;
            case SideType.Up:
                return SideType.Down;
            case SideType.Down:
                return SideType.Up;
        }
        return SideType.Back;
    }
    public static SideType RotSideType(SideType type, bool right = true)
    {
        switch (type)
        {
            case SideType.Forward:
                if (right)
                    return SideType.Right;
                else
                    return SideType.Left;
            case SideType.Back:
                if (right)
                    return SideType.Left;
                else
                    return SideType.Right;
            case SideType.Right:
                if (right)
                    return SideType.Back;
                else
                    return SideType.Forward;
            case SideType.Left:
                if (right)
                    return SideType.Forward;
                else
                    return SideType.Back;
            case SideType.Down:
                if (right)
                    return SideType.Right;
                else
                    return SideType.Left;
            case SideType.Up:
                if (right)
                    return SideType.Left;
                else
                    return SideType.Right;
            default:
                return SideType.Forward;
        }
    }
}
