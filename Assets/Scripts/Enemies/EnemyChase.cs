using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float detectionRange = 10f;
    public float stopRange = 2f;

    private UnityEngine.AI.NavMeshAgent agent;
    private Enemy enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyScript = GetComponent<Enemy>();
        animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // controllo per il knockback
        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
        {
            return;
        }

        // altri controlli
        if (enemyScript != null && enemyScript.IsDying()) return; 
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (distance > stopRange)
            {
                animator.SetBool("isMoving", distance > stopRange);
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
                enemyScript.TryAttackPlayer(player);
                // si puo aggiungere animazione di attacco o trigger
            }
        }
        else
        {
            agent.ResetPath();
        }
    }
}
