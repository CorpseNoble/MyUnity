using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GenPr3
{
    public class RoomElementScript : MonoBehaviour
{
    public RoomElement element = RoomElement.MainFloor;
    public List<Orientation> orientations = new List<Orientation>();
}
}
