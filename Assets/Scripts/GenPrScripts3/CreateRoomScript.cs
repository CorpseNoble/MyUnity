using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace GenPr3
{
    [ExecuteInEditMode]
public class CreateRoomScript : MonoBehaviour
{
    [Range(1, 20)]
    public int roomSize = 1;
    public PropertyButton CreateCube;

    [Range(1, 20)]
    public int countX = 1;
    [Range(1, 20)]
    public int countZ = 1;
    [Range(1, 20)]
    public int countY = 1;
    public PropertyButton CreateCustom;


    public List<RoomElementScript> roomElements = new List<RoomElementScript>();

    public void Awake()
    {
        Istall();
    }
    private void Istall()
    {
        CreateCube = new PropertyButton(() => CreateCubeRoom(roomSize));
        CreateCustom = new PropertyButton(() => CreateCustomRoom(countX, countY, countZ));
    }
    public void OnEnable()
    {
        Istall();
    }

    public void CreateCubeRoom(int size)
    {
        Debug.Log($"CreateCubeRoom({size})");
        CreateCustomRoom(size, size, size);
    }

    public void CreateCustomRoom(int countX, int countY, int countZ)
    {
        Debug.Log($"CreateCustomRoom({countX}, {countY}, {countZ})");

        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = new Vector3(countX / 2f, countY / 2f, countZ / 2f);
        obj.transform.localScale = new Vector3(countX, countY, countZ);
    }

    private void CreateFloor(int countX, int countZ)
    {

    }

    private void CreateWall(Orientation orientation, int hight, int width)
    {

    }
}
}
