using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // This is the namespace for TextMesh Pro.

public class BareelGrab : MonoBehaviour
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
            if (hit.transform.CompareTag("GrabbableBarrel") && !isHoldingObject)
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
            if (isHoldingObject)
            {
                // Release the object
                currentGrabbableObject.transform.SetParent(null);
                isHoldingObject = false;
            }
            else
            {
                // Grab the object
                currentGrabbableObject.transform.SetParent(this.transform);
                isHoldingObject = true;
            }
        }
    }
}
