using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GenPr1
{
    public enum SideType
    {
        Forward,
        Back,
        Right,
        Left,
        Up,
        Down,
    }

    public enum WayType
    {
        OpenDoor,
        Area,
        Other,
    }

    public enum PatternParamType
    {
        end,
        branch,
        path,
        twist,
        turnR,
        turnL,
        down,
        up,
    }

    public enum TriggerType
    {
        AttackTrigger,
        ReadyAttackTrigger,
        TargetTrigger,
    }

    public enum ControllerType
    {
        Player,
        Monster,
    }

    public enum MonsterStay
    {
        Idle,
        Go,
        Attack,
        Spec,
    }

    public enum TypeSize
    {
        Big,
        Normal,
        Small,
    }

    public enum SlameType
    {
        Boss,
        Satellite,
    }

    public enum GenType
    {
        Random,
        Pattern,
        Mixed,
    }
}