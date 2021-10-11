using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{

    public Animator animator;

    NavMeshAgent navmeshAgent;
    CharacterCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed Percent", navmeshAgent.velocity.magnitude / navmeshAgent.speed, .1f, Time.deltaTime);
    }

    void OnAttack()
    {
        animator.SetTrigger("Attack");
    }
}
