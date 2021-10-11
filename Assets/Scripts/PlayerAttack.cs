using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    CharacterStats myStats;

    // Start is called before the first frame update
    void Start()
    {
        animator = animator.GetComponent<Animator>();
        myStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetBool("attack", true);

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            if (enemy.tag == "Barrel")
            {
                if (myStats.damage.GetValue() > 0f)
                    enemy.GetComponent<BarrelDestruction>().destroyBarrel();
            }
            if (enemy.tag == "Monster")
            {
                enemy.GetComponent<Enemy>().TakeDamage();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
