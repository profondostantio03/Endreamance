using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Transform player;
    private NavMeshAgent agent;
    private Enemy enemyScript;

    [Header("Settings AI")]
    public float detectionRange = 10f;
    public float stopRange = 2f;

    private float pathUpdateTimer = 0f;
    public float pathUpdateDelay = 0.25f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<Enemy>();

        // tenta di trovare l'animator
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (agent != null) agent.autoBraking = true;
    }

    void Update()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;

        if (enemyScript != null && enemyScript.IsDying())
        {
            agent.isStopped = true;
            agent.ResetPath();
            return;
        }

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            // INSEGUIMENTO
            if (distance > stopRange)
            {
                // Se l'animator esiste, lo usa. Se è null, SALTA questa riga senza errori
                if (animator != null) animator.SetBool("isMoving", true);

                pathUpdateTimer += Time.deltaTime;
                if (pathUpdateTimer >= pathUpdateDelay)
                {
                    agent.SetDestination(player.position);
                    pathUpdateTimer = 0f;
                    agent.autoBraking = false;
                }
            }
            // ATTACCO
            else
            {
                if (!agent.isStopped) agent.ResetPath();
                if (animator != null) animator.SetBool("isMoving", false);
                FaceTarget();

                enemyScript.TryAttackPlayer(player);
            }
        }
        // IDLE
        else
        {
            if (!agent.isStopped) agent.ResetPath();

            if (animator != null) animator.SetBool("isMoving", false);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}