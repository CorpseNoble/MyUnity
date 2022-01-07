using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class GenVer : MonoBehaviour
{
    public abstract int GetParam { get; }
    public abstract bool GetDoor { get; }
    public abstract bool GetLoop { get; }
    public abstract IEnumerable<RoomSide> GetParam2(RoomSide side);
    public abstract void Clear();
    public abstract void LeadUp();
}
