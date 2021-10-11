using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    public int ID;

    public Animator animator;

    public float deathDelay = 4f;

    PlayerStats playerStats;

    public delegate void EnemyEventHandler(int ID);
    public static event EnemyEventHandler OnEnemyDeath;

    private void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }

    public override void Die()
    {
        base.Die();

        animator.SetBool("Dead", true);

        if (OnEnemyDeath != null)
        {
            OnEnemyDeath(ID);
        }

        StartCoroutine(Destroy(deathDelay));

    }

    IEnumerator Destroy(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }

}
