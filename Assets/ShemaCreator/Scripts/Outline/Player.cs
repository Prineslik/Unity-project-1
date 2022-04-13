using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public Interactable currentInteractable;
    //private Interactable previousInteractable;

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Interactable interactable;

        //if (Physics.Raycast(ray, out hit, 1000000))
        //{
        //    interactable = hit.collider.GetComponent<Interactable>();
        //    if (interactable)
        //    {
        //        if (interactable != previousInteractable)
        //        {
        //            print("OnHoverEnter");
        //        }
        //    }
        //    else if (previousInteractable != null)
        //    {
        //        print("OnHoverExit");
        //    }
        //}
        //else if (previousInteractable != null)
        //{
        //    print("nothing");
        //}

        if (Physics.Raycast(ray, out hit, 1000000))
        {
            interactable = hit.collider.GetComponent<Interactable>();
            if (interactable)
            {
                if (currentInteractable && currentInteractable != interactable)
                {
                    currentInteractable.OnHoverExit();
                }
                currentInteractable = interactable;
                interactable.OnHoverEnter();

                // Õ¿∆¿“»≈
                if (Input.GetMouseButtonUp(0))
                {
                    interactable.OnClickHandler();
                }
            }
            else
            {
                if (currentInteractable)
                {
                    currentInteractable.OnHoverExit();
                    currentInteractable = null;
                }
            }
        }
        else
        {
            if (currentInteractable)
            {
                currentInteractable.OnHoverExit();
                currentInteractable = null;
            }
        }
    }
}
