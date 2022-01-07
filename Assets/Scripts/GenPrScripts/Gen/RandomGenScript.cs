using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RandomGenScript : GenVer
{
    public int seed = 0;
    [Space(10f)]
    [Range(0, max)]//0
    public int ends = 1;
    [Range(0, max)]//1
    public int branch = 1;
    [Range(0, max)]//2
    public int path = 1;
    [Range(0, max)]//3
    public int twist = 1;
    [Range(0, max)]//4
    public int turnR = 1;
    [Range(0, max)]//5
    public int turnL = 1;
    [Range(0, max)]//6
    public int down = 0;
    [Range(0, max)]//7
    public int up = 0;


    [Range(0, max)]//6
    public int height = 1;
    [Range(0, max)]
    public int loop = 1;
    [Range(0, max)]
    public int door = 1;

    private const int max = 20;
    private System.Random startRand = new System.Random();
    private System.Random mainRand;

    public override int GetParam
    {
        get => Coin(ends, branch, path, twist, turnR, turnL, down, up);
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
        seed = 0;
    }

    public override void LeadUp()
    {
        if (seed == 0)
            seed = startRand.Next(Int32.MinValue, Int32.MaxValue);

        mainRand = new System.Random(seed);
    }

    private bool ThrowCoin(int count)
    {
        if (mainRand.Next(0, max) < count)
            return true;

        return false;
    }
    private int Coin(params int[] ps)
    {
        var d = mainRand.Next(0, ps.Sum());
        var di = 0;
        var s = 0;
        for (int i = 0; i < ps.Length; i++)
        {
            if (d >= s && d < s + ps[i])
            {
                di = i;
                return di;
            }
            s += ps[i];
        }
        return di;
    }
}
