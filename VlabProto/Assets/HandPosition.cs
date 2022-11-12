using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandPosition : MonoBehaviour
{
    [SerializeField] GameObject hand;
    [SerializeField] Transform position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // hand.transform.position = position.position;
        //MousePosition();
    }

    void MousePosition()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hand.transform.position =new Vector3 (worldPosition.x , worldPosition.y , 0f);
        Debug.Log(worldPosition);
    }
}
