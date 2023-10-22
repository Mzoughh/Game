using UnityEngine;

public class Key2Collectible : MonoBehaviour
{
    public GameObject[] barrels;       // Drag the 4 barrels here.
    public GameObject[] fires;         // Drag the 4 fire positions here.
    public GameObject secretDoor;      // Drag your secret door here.
    private int barrelsPlaced = 0;     // To keep track of barrels placed correctly.

    private void Start()
    {
        foreach (GameObject barrel in barrels)
        {
            barrel.SetActive(false);
        }

        foreach (GameObject fire in fires)
        {
            fire.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.CompareTag("Key2"))
            {
                CollectKey2();
            }

            GameManager.instance.KeyCollected(); // Notify the GameManager that a key has been collected.

            this.gameObject.SetActive(false);
        }
    }

    void CollectKey2()
    {
        foreach (GameObject barrel in barrels)
        {
            barrel.SetActive(true);
        }

        foreach (GameObject fire in fires)
        {
            fire.SetActive(true);
            // Attach the BarrelPlacement script to each fire (discussed below).
            fire.AddComponent<BarrelPlacement>().Initialize(this);
        }
    }

    // This method is called from the BarrelPlacement script.
    public void BarrelCorrectlyPlaced()
    {
        barrelsPlaced++;
        if (barrelsPlaced == 4) // All barrels are placed correctly.
        {
            // Logic to open the secret door.
            OpenSecretDoor();
        }
    }

    void OpenSecretDoor()
    {
        // Assuming the door just gets deactivated to "open" it.
        // You can replace this with any animation or other logic you have for the door.
        secretDoor.SetActive(false);
    }
}

public class BarrelPlacement : MonoBehaviour
{
    private Key2Collectible key2Script;
    private Collider myCollider; // Collider of the fire

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    public void Initialize(Key2Collectible script)
    {
        key2Script = script;
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("Grabbable"))
        {

            // Lock the barrel in place.
            LockBarrel(other.gameObject);

            // Extinguish fire.
            this.gameObject.SetActive(false);
            key2Script.BarrelCorrectlyPlaced();
        }
    }

    private void LockBarrel(GameObject barrel)
    {
        // Set barrel position to the fire's position.
        barrel.transform.position = this.transform.position;

        // Unparent the barrel, if it's a child of another object (like the player).
        barrel.transform.SetParent(null);

        // Mark the barrel as locked.
        BarrelState barrelState = barrel.GetComponent<BarrelState>();
        if (barrelState)
        {
            barrelState.isLocked = true;
        }

        // Optionally, you can also disable the collider on the barrel so it no longer interacts.
        Collider barrelCollider = barrel.GetComponent<Collider>();
        if (barrelCollider)
        {
            barrelCollider.enabled = false;
        }

        PlayerGrab playerGrab = FindObjectOfType<PlayerGrab>();
        if (playerGrab)
        {
            playerGrab.ReleaseObject();
        }
    }
}




