using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton pattern.

    public TextMeshProUGUI keyCounterText;  // Drag your UI Text component here to show "X/3 keys collected".

    private int keysCollected = 0;

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

    private void Start()
    {
        UpdateKeyCounter();
    }

    private void Update()
    {
        // Check for game restart.
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void KeyCollected()
    {
        keysCollected++;
        UpdateKeyCounter();
    }

    private void UpdateKeyCounter()
    {
        keyCounterText.text = "Keys collected : " + keysCollected + "/3";
    }

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
