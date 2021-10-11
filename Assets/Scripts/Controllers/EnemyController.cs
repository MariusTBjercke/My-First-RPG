using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    EnemyStats stats;

    // Patrol
    public Transform[] waypoints;
    public int speed;
    float time = 0f;
    public float waypointDelay = 4f;

    private int waypointIndex;
    private float dist;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<EnemyStats>();
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (stats.currentHealth > 0)
        {
            if (distance <= lookRadius)
            {

                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance + 3f)
                {
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();
                    if (targetStats != null)
                    {
                        combat.Attack(targetStats);
                    }
                    FaceTarget();
                }

            }
            else
            {
                // Patrol
                dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
                if (dist < 4f)
                {
                    // Wait for seconds then increase array
                    time = time + 1f * Time.deltaTime;
                    if (time >= waypointDelay)
                    {
                        time = 0f;
                        IncreaseIndex();
                    }
                }
                Patrol();
            }
        }
    }

    void Patrol()
    {
        agent.SetDestination(waypoints[waypointIndex].position);
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex]);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
