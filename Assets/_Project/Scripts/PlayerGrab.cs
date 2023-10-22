using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // This is the namespace for TextMesh Pro.

public class PlayerGrab : MonoBehaviour
{
    public float grabDistance = 2.0f;
    public TextMeshProUGUI instructionText;  // This is the TMP text component for UI.

    private GameObject currentGrabbableObject;
    private bool isHoldingObject = false;

    void Start()
    {
        instructionText.text = "";
    }

    void Update()
    {
        CheckForGrabbableObject();
        HandleGrabRelease();
    }

    private void CheckForGrabbableObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance))
        {
            if (hit.transform.CompareTag("Grabbable") && !isHoldingObject)
            {
                instructionText.text = "Press SHIFT to grab the object.";
                currentGrabbableObject = hit.transform.gameObject;
            }
        }
        else
        {
            instructionText.text = "";
            currentGrabbableObject = null;
        }
    }

    private void HandleGrabRelease()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentGrabbableObject)
        {
            BarrelState barrelState = currentGrabbableObject.GetComponent<BarrelState>();
            if (barrelState && barrelState.isLocked)
            {
                // If the barrel is locked, we simply return and don't proceed with the grab/release logic.
                return;
            }
            if (isHoldingObject)
            {
                // Release the object
                currentGrabbableObject.transform.SetParent(null);
                isHoldingObject = false;

                // Reset the tag to "Barrel" (if it was changed elsewhere)
                currentGrabbableObject.tag = "Grabbable";
            }
            else
            {
                // Grab the object
                currentGrabbableObject.transform.SetParent(this.transform);
                isHoldingObject = true;
            }
        }
    }

    public void ReleaseObject()
    {
        if (isHoldingObject && currentGrabbableObject)
        {
            currentGrabbableObject.transform.SetParent(null);
            isHoldingObject = false;
        }
        currentGrabbableObject = null;
    }
}
