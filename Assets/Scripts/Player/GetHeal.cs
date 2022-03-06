using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Player
{

    public class GetHeal : MonoBehaviour
    {
        public int HealValue = 100;
        public DamageAcceptor acceptor = DamageAcceptor.Player;
    }
}