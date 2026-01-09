using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importante per usare NavMeshAgent senza scrivere tutto il percorso

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

    // Variabili per l'ottimizzazione
    private float pathUpdateTimer = 0f;
    public float pathUpdateDelay = 0.25f; // Aggiorna il percorso ogni 0.25 secondi

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<Enemy>();

        // Se l'animator non è assegnato nell'inspector, prova a prenderlo
        if (animator == null) animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        // Impostiamo l'auto braking per evitare che scivoli oltre il punto di stop
        if (agent != null) agent.autoBraking = true;
    }

    void Update()
    {
        // 1. Controlli di sicurezza (Agent attivo, su NavMesh, non morto)
        if (agent == null || !agent.enabled || !agent.isOnNavMesh) return;
        if (enemyScript != null && enemyScript.IsDying())
        {
            // Se sta morendo, assicuriamoci che si fermi
            agent.isStopped = true;
            agent.ResetPath();
            return;
        }
        if (player == null) return;


        // 2. Calcolo Distanza
        float distance = Vector3.Distance(transform.position, player.position);

        // 3. Logica di Inseguimento
        if (distance <= detectionRange)
        {
            // --- FASE DI INSEGUIMENTO ---
            if (distance > stopRange)
            {
                animator.SetBool("isMoving", true);

                // OTTIMIZZAZIONE: Non ricalcolare il percorso ad ogni frame!
                pathUpdateTimer += Time.deltaTime;
                if (pathUpdateTimer >= pathUpdateDelay)
                {
                    agent.SetDestination(player.position);
                    pathUpdateTimer = 0f;
                    agent.autoBraking = false;
                }
            }
            // --- FASE DI ATTACCO ---
            else
            {
                // Siamo vicini: fermati e attacca
                if (!agent.isStopped) agent.ResetPath();

                animator.SetBool("isMoving", false);

                // Importante: Girati verso il player mentre attacchi
                FaceTarget();

                enemyScript.TryAttackPlayer(player);
            }
        }
        // --- FASE IDLE (Player lontano) ---
        else
        {
            if (!agent.isStopped) agent.ResetPath();
            animator.SetBool("isMoving", false);
        }
    }

    // Funzione extra per ruotare fluidamente verso il player quando si è fermi
    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        // Ignora l'altezza per evitare che il nemico si inclini guardando in basso/alto
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // Ruota dolcemente (Slerp) invece di scattare
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}