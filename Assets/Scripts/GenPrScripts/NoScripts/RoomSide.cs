using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AllSide
{
    public RoomSide[] AllSides => allSides;

    private RoomSide[] allSides;
    public RoomSide Forward => forward;

    private RoomSide forward;
    public RoomSide Back => back;

    private RoomSide back;
    public RoomSide Right => right;

    private RoomSide right;
    public RoomSide Left => left;

    private RoomSide left;
    public RoomSide Up => up;

    private RoomSide up;
    public RoomSide Down => down;

    private RoomSide down;

    public AllSide()
    {
        forward = new RoomSide(SideType.Forward, Vector3.forward);
        back = new RoomSide(SideType.Back, Vector3.back);
        right = new RoomSide(SideType.Right, Vector3.right);
        left = new RoomSide(SideType.Left, Vector3.left);
        up = new RoomSide(SideType.Up, Vector3.up);
        down = new RoomSide(SideType.Down, Vector3.down);

        forward.revertSide = Back;
        forward.leftSide = Left;
        forward.rightSide = Right;
        forward.upSide = Up;
        forward.downSide = Down;

        back.revertSide = Forward;
        back.leftSide = Right;
        back.rightSide = Left;
        back.upSide = Up;
        back.downSide = Down;

        right.revertSide = Left;
        right.leftSide = Forward;
        right.rightSide = Back;
        right.upSide = Up;
        right.downSide = Down;

        left.revertSide = Right;
        left.leftSide = Back;
        left.rightSide = Forward;
        left.upSide = Up;
        left.downSide = Down;

        up.revertSide = Down;
        up.leftSide = Left;
        up.rightSide = Right;
        up.upSide = Back;
        up.downSide = Forward;

        down.revertSide = Up;
        down.leftSide = Left;
        down.rightSide = Right;
        down.upSide = Forward;
        down.downSide = Back;


        allSides = new RoomSide[]
                {
                    Forward,
                    Back,
                    Right,
                    Left,
                    Up,
                    Down,
                };
    }
}

public class RoomSide
{
    public SideType side;
    public Vector3 vector;
    public RoomSide revertSide;
    public RoomSide rightSide;
    public RoomSide leftSide;
    public RoomSide upSide;
    public RoomSide downSide;

    public RoomSide(SideType side, Vector3 vector)
    {
        this.side = side;
        this.vector = vector;

    }
}