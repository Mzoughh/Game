using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public float deathThreshold = -10f; // Adjust this value based on your level.
    public Transform respawnPoint;      // Drag and drop the respawn point (empty GameObject) from the editor.

    private Transform playerTransform;

    void Start()
    {
        // Assuming your player has the tag "Player"
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        CheckForDeath();
    }

    void CheckForDeath()
    {
        if (playerTransform.position.y < deathThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        playerTransform.position = respawnPoint.position;

        // If your player has a rigidbody and momentum causes continuous death, reset it.
        Rigidbody rb = playerTransform.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.velocity = Vector3.zero;
        }
    }
}

