using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : AliveController
{
    protected new void Start()
    {
        base.Start();
        acceptor = DamageAcceptor.Player;
    }
}
