using System.Collections;
using UnityEngine;

public class GetDamage : MonoBehaviour
{
    public int DamageValue = 100;
    public DamageAcceptor acceptor = DamageAcceptor.Player;
}

public enum DamageAcceptor 
{
    Player,
    Enemy,
}