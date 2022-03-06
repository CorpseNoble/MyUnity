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
            get => playerInventory.status.HP.ValueMax;
            set
            {
                base.MaxHealth = value;
                playerInventory.status.HP.ValueMax = value;
            }
        }
        public override int Health 
        { 
            get => playerInventory.status.HP.ValueCurrent; 
            set 
            {
                base.Health = value;
                playerInventory.status.HP.ValueCurrent = value;
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
