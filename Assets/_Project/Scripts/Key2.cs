using UnityEngine;

public class Key2 : MonoBehaviour
{
    public GameObject key2;            // Drag the key2 object from your scene here.
    public GameObject[] boxes;         // Drag the 3 boxes from your scene here.

    private void Start()
    {
        // Ensure key2 and the boxes start as inactive.
        key2.SetActive(false);

        foreach (GameObject box in boxes)
        {
            box.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player object has a tag named "Player".
        {
            if (this.CompareTag("Key2"))
            {
                CollectKey1();
            }

            // Hide the key after collection.
            this.gameObject.SetActive(false);
        }
    }

    void CollectKey1()
    {
        // Activate key2.
        key2.SetActive(true);

        // Activate the 3 boxes.
        foreach (GameObject box in boxes)
        {
            box.SetActive(true);
        }
    }
}

