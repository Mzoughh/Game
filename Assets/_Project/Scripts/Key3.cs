using UnityEngine;

public class Key3 : MonoBehaviour
{
    public GameObject[] walls;    // Drag in the walls that should disappear.
    public GameObject key3;       // Drag your Key3 object here.
    public GameObject light;

    private void Start()
    {
        // Ensure key2 and the boxes start as inactive.
        light.SetActive(false);

    
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Make walls disappear.
            foreach (GameObject wall in walls)
            {
                wall.SetActive(false);
            }

            // Make Key3 disappear.
            GameManager.instance.KeyCollected(); // Notify the GameManager that a key has been collected.

            key3.SetActive(false);
            light.SetActive(true);
        }
    }
}
