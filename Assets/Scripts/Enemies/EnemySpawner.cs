using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnAreas;
    public float spawnInterval = 5f;
    public float spawnDistance = 15f;
    public Transform player;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return; // Verifica che ci sia un riferimento al giocatore
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            bool playerNearSpawnArea = false;
            foreach (Transform area in spawnAreas)
            {
                float distanceToPlayer = Vector3.Distance(player.position, area.position);
                if (distanceToPlayer <= spawnDistance) {
                    playerNearSpawnArea = true;
                    break;
            }
            }
            if (playerNearSpawnArea)
            {
                SpawnEnemy();
            }
            timer = 0f;
        }

        void SpawnEnemy()
        {
            Transform area = spawnAreas[Random.Range(0, spawnAreas.Length)];
            Vector3 center = area.position;
            Vector3 size = area.localScale;

            Vector3 spawnPos = center + new Vector3(
                Random.Range(-size.x / 2f, size.x / 2f),
                0f,
                Random.Range(-size.z / 2f, size.z / 2f)
            );

            // Regola l’altezza se serve
            spawnPos.y = center.y;

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }

        void OnDrawGizmosSelected()
        {
            if (spawnAreas == null) return;
            Gizmos.color = Color.cyan;
            foreach (Transform area in spawnAreas)
            {
                if (area != null)
                    Gizmos.DrawWireCube(area.position, area.localScale);
            }
        }
    }
}
