using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInOneDirection : MonoBehaviour
{
    public enum Direction { 
        Forward, 
        Backward, 
        Left, 
        Right 
    }
    public Direction direction = Direction.Backward;
    public float pushDistance = 8f;
    public float pushTime = 0.2f; // durata della spinta in secondi
    public bool localDirection = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        StartCoroutine(PushPlayer(collision.gameObject.transform));
    }

    private IEnumerator PushPlayer(Transform player)
    {
        Vector3 dir = Vector3.zero;
        switch (direction)
        {
            case Direction.Forward: 
                dir = localDirection ? transform.forward : Vector3.forward; 
                break;
            case Direction.Backward: 
                dir = localDirection ? -transform.forward : Vector3.back; 
                break;
            case Direction.Left: 
                dir = localDirection ? -transform.right : Vector3.left; 
                break;
            case Direction.Right: 
                dir = localDirection ? transform.right : Vector3.right; 
                break;
        }

        dir.y = 0f;
        dir.Normalize();

        Vector3 start = player.position;
        Vector3 target = start + dir * pushDistance;
        float elapsed = 0f;

        while (elapsed < pushTime)
        {
            player.position = Vector3.Lerp(start, target, elapsed / pushTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        player.position = target;
    }
}
