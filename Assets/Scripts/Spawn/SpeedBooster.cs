using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    public float speedMultiplier = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var moveScript = other.GetComponent<PlayerMovementAndCamera>();
            if (moveScript != null)
            {
                moveScript.SetSpeedMultiplier(speedMultiplier);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var moveScript = other.GetComponent<PlayerMovementAndCamera>();
            if (moveScript != null)
            {
                moveScript.ResetSpeedMultiplier();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
