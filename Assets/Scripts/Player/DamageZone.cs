using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int DamageValue = 100;
    public float Multi = 1;
    public DamageAcceptor acceptor = DamageAcceptor.Player;
    public List<AliveController> hitReserver = new List<AliveController>();
    public AudioSource audioSource;
    public AudioClip audioClip;
    public bool Zone => _zone;
    [SerializeField] protected bool _zone = false;
    public void GiveGamege(AliveController alive)
    {
        if (Multi <= 0)
            return;

        if (!Zone)
        {
            if (hitReserver.Contains(alive))
                return;
            hitReserver.Add(alive);
        }

        alive.GetDamage((int)(DamageValue * Multi));


        if (audioSource == null)
            return;
        audioSource.PlayOneShot(audioClip);
    }
    public void EndHit(out int countHit)
    {
        countHit = hitReserver.Count;
        hitReserver.Clear();
    }
}

public enum DamageAcceptor
{
    Player,
    Enemy,
}