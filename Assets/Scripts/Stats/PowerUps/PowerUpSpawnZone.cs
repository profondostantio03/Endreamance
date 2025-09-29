using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnZone : MonoBehaviour
{
    public GameObject[] oggettiDaSpawnare;
    public GameObject zonaDiSpawn;
    public int nOggetti = 10;

    private Collider areaSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (zonaDiSpawn != null)
        {
            areaSpawn = zonaDiSpawn.GetComponent<Collider>();
            if (areaSpawn == null)
            {
                Debug.LogError("Il GameObject deve avere un collider");
                return;
            }
            SpawnPowerUps();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPowerUps()
    {
        for (int i = 0; i < nOggetti; i++)
        {
            Vector3 randomPos = genRandomPos();
            GameObject prefabRandom = oggettiDaSpawnare[Random.Range(0, oggettiDaSpawnare.Length)];
            Instantiate(prefabRandom, randomPos, Quaternion.identity);
        }
    }

    Vector3 genRandomPos() //PER GENERARE UNA POSIZIONE CASUALE
    {
        Bounds bounds = areaSpawn.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.center.y; // per spawnare a terra, altrimenti Random.Range(bounds.min.y, bounds.max.y)
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }

}
