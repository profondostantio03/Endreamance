using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupSpawn : MonoBehaviour
{
    [Header("Damage Popup Settings")]
    public GameObject damagePopupPrefab;
    public float popupSpread = 1.5f;  
    public float popupHeight = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPopup(int damage, Vector3 position)
    {
        if (damagePopupPrefab == null) return;

        Camera cam = Camera.main;

        Vector3 randomOffset =
                (cam.transform.right * Random.Range(-popupSpread, popupSpread)) +
                (cam.transform.up * Random.Range(-popupSpread, popupSpread));

        Vector3 spawnPosition = position + Vector3.up * popupHeight + randomOffset;

        GameObject popup = Instantiate(damagePopupPrefab, spawnPosition, Quaternion.identity);

        DamagePopup dp = popup.GetComponent<DamagePopup>();
        if (dp != null)
        {
            dp.Setup(damage);
        }
    }

}
