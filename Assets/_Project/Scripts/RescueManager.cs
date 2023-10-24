using System.Collections;
using UnityEngine;
using TMPro;  // TextMesh Pro Namespace.

public class RescueManager : MonoBehaviour
{
    public static RescueManager instance;  // Singleton pattern.

    public TextMeshProUGUI rescueText;  // Drag your UI Text component here.
    public GameObject key3;       // Drag your Key3 prefab here.
    public Transform teleportPoint;     // Point to teleport player and characters to.

    public GameObject player;

    public GameObject[] characters;    // Drag both your characters here.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        key3.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        rescueText.gameObject.SetActive(false);
    }

    public void StartRescueSequence()
    {
        StartCoroutine(RescueSequence());
    }


    IEnumerator RescueSequence()
    {
        rescueText.text = "Thank you for saving us! Now let's run away!";
        rescueText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);

        rescueText.gameObject.SetActive(false);

        // Activate Key3
        key3.SetActive(true);



        foreach (GameObject character in characters)
        {
            character.transform.position = teleportPoint.position;
        }

        // Teleport player and characters.
        player.transform.position = teleportPoint.position;
        
    }
}
