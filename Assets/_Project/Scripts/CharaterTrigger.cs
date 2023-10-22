using UnityEngine;

public class CharacterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the rescue sequence.
            RescueManager.instance.StartRescueSequence();

            // Disable this trigger to ensure the sequence doesn't start again.
            GetComponent<Collider>().enabled = false;
        }
    }
}

