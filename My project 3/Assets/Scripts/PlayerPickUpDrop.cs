using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{

    [SerializeField] private float pickDistance = 3f;
    [SerializeField] private LayerMask pickableLayer;
    [SerializeField] private Transform grabPoint;
    private  ObjectGrabbable _grabbedObject;
    void Update()
    {
        //if e is down
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (_grabbedObject == null)
            {
                //raycast
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,
                        pickDistance, pickableLayer))
                {
                    if (hit.transform.TryGetComponent(out _grabbedObject))
                    {
                        Debug.Log("grab");
                        _grabbedObject.Grab(grabPoint);
                    }
                }
            }
            else
            {
                Debug.Log("drop");
                _grabbedObject.Drop();
                _grabbedObject = null;
            }
        }

        if (_grabbedObject != null && Vector3.Distance(_grabbedObject.transform.position, grabPoint.transform.position) > 1.5f)
        {
            _grabbedObject.Drop();
            _grabbedObject = null;
        }
    }
}
