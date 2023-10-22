using UnityEngine;
using TMPro;  // Make sure you have the TextMesh Pro package installed.

public class VictoryTrigger : MonoBehaviour
{
    public TextMeshProUGUI victoryText;  // Drag your UI Text component here.

    private void Start()
    {
        // Make sure the victory text is hidden at the start.
        victoryText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Display the victory text.
            victoryText.gameObject.SetActive(true);
            victoryText.text = "Congratulations! You've won! Press R to restart";
        }
    }
}
