using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    RaycastHit hit;
    public GameObject sample;

    [SerializeField]
    private GameObject placeableObject;
    [SerializeField]
    private KeyCode newHotkey = KeyCode.A;
    private GameObject currentPlaceableObject;
    float mouseWheelRotation;
    // Update is called once per frame
    void Update()
    {
         HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveObjectUsingMouse();
        }
            
           //   RotationUsingMouse();
             ReleaseIfClicked();
           

    }

     void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }
    }

   // void RotationUsingMouse()
    //{
      //  mouseWheelRotation = Input.mouseScrollDelta.y;
        //currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    //}

     void MoveObjectUsingMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            currentPlaceableObject.transform.position = hit.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

     void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newHotkey))
        {
            if (currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(placeableObject);
            }
            else
            {
                Destroy(currentPlaceableObject);
            }
        }

    }
}
