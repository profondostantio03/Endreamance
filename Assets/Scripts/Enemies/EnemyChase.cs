using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
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
        if (enemyScript != null && enemyScript.IsDying()) return; 
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (distance > stopRange)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
                // si puo aggiungere animazione di attacco o trigger
            }
        }
        else
        {
            agent.ResetPath();
        }
    }
}
