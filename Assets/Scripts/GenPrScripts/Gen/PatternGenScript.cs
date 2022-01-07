using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

public class PatternGenScript : GenVer
{
    public List<Block> parameters = new List<Block>();
    public List<Block2> loop = new List<Block2>();
    public List<Block2> door = new List<Block2>();
    private int patIter = 0;

    public override int GetParam
    {
        get => Coin();
    }

    public override bool GetDoor
    {
        get => ThrowCoin(door);
    }

    public override bool GetLoop
    {
        get => ThrowCoin(loop);
    }

    public override void Clear()
    {
        patIter = 0;
    }

    public override void LeadUp()
    {
        if (parameters.Count == 0)
            parameters.Add(new Block());
        if (loop.Count == 0)
            loop.Add(new Block2());
        if (door.Count == 0)
            door.Add(new Block2());
    }

    private bool ThrowCoin(List<Block2> vs)
    {
        var s = 0;
        bool par = false;
        var Iteri = patIter % vs.Select(c => c.count).Sum();
        for (int i = 0; i < vs.Count; i++)
        {
            if (Iteri >= s && Iteri < vs[i].count + s)
            {
                par = vs[i].value;
                return par;
            }
            s += vs[i].count;
        }
        return par;
    }

    private int Coin()
    {
        var s = 0;
        PatternParamType par = 0;
        patIter %= parameters.Select(c => c.value).Sum();
        for (int i = 0; i < parameters.Count; i++)
        {
            if (patIter >= s && patIter < parameters[i].value + s)
            {
                par = parameters[i].type;
                break;
            }
            s += parameters[i].value;
        }
        patIter++;
        return (int)par;
    }

    public override IEnumerable<RoomSide> GetParam2(RoomSide side)
    {
        switch (GetParam)
        {
            case 0:
                return new List<RoomSide>();
            case 1:
                return new List<RoomSide>() { side.rightSide, side.leftSide, side };
            case 2:
                return new List<RoomSide>() { side };
            case 3:
                return new List<RoomSide>() { side.rightSide, side.leftSide };
            case 4:
                return new List<RoomSide>() { side.rightSide };
            case 5:
                return new List<RoomSide>() { side.leftSide };
            case 6:
                return new List<RoomSide>() { side.downSide };
            case 7:
                return new List<RoomSide>() { side.upSide };
            default:
                return new List<RoomSide>();
        }

    }
}

[Serializable]
public class Block
{
    public PatternParamType type;
    [Range(1, 16)]
    public int value = 1;
}

[Serializable]
public class Block2
{
    public bool value = true;
    [Range(1, 16)]
    public int count = 1;
}

