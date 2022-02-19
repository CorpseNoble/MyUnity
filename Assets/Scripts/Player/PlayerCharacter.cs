using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : AliveController
{
    protected new void Start()
    {
        base.Start();
        acceptor = DamageAcceptor.Player;
    }
    protected override void Death()
    {
        base.Death();
        Debug.Log("You Dead");
        Invoke(nameof(Restart), 2f);
    }

    protected void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
