using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : MonoBehaviour
{
    public AudioPlayer audioPlayer;

    private int damage;
    private bool slowDown;
    private AudioClip hitSound;

    public void SetAttack(Hit hit)
    {
        damage = hit.damage;
        slowDown = hit.slowDown;
        hitSound = hit.hitSound;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Life enemy = collision.GetComponent<Life>();
        if(enemy != null && collision.gameObject.CompareTag("Enemy"))
        {
            enemy.TakeDamage(damage);
            audioPlayer.PlaySound(hitSound); //botei na main camera porque no player ficava o ícone de áudio
                                             //preciso descobrir o pq
            if(slowDown)
               SlowDownEffect.instance.SetSlowDown();
            
            ComboManager.instance.SetCombo();
        }
    }
}
