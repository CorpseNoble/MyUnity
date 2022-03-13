using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Inventory;


namespace Assets.Scripts.Player
{
    public class PlayerCharacter : AliveController
    {
        public PlayerInventoryData playerInventory;
        public override int MaxHealth 
        { 
            get => playerInventory.status[BarStatType.HP].Value;
            set
            {
                base.MaxHealth = value;
                playerInventory.status[BarStatType.HP].Value = value;
            }
        }

        protected override void Start()
        {
            base.Start();
        }
        protected override void Death()
        {
            base.Death();
            Debug.Log("You Dead");
        }
    }
}
