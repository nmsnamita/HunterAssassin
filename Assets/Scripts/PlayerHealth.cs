using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float hitPoints = 100f;
    [SerializeField] GameObject deathParticles;

    PlayerMovement player;
    EnemyMovement enemy;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        enemy = FindObjectOfType<EnemyMovement>();
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            hitPoints = 0;
            player.animator.SetTrigger("Die");
            player.hasDied = true;
            Instantiate(deathParticles, transform.position, Quaternion.identity);

            // enemy.animator.SetBool("Attack", false);
        }
    }
}
