using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubSelectLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if Left mouse button is pressed and the mouse is not on an UI element.
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }
    }

    private void GetInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        // Check for collision from mouse position to infinity.
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {

            GameObject interactedObject = interactionInfo.collider.gameObject;

            if (interactedObject.tag == "LevelSelector")
            {
                Debug.Log("Clicked on a level with name of : " + interactedObject.name);
            }

        }
    }
}
