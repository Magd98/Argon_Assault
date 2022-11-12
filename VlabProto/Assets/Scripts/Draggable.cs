using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Draggable : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private float distance = 2.5f;
   // private Color _objectColor;
    private Camera _mainCamera;
    private Rigidbody _rb;
    private Renderer _renderer;
    private SphereCollider _collider;
    private bool _isBeingDragged;
    private float _distanceToCamera;
    //static bool finished;
    [SerializeField] GameObject hand,hold;
    
    private void Start()
    {

        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _isBeingDragged = false;
       // _objectColor = _renderer.material.color;
        _collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        _distanceToCamera = Vector3.Distance(_mainCamera.transform.position, transform.position);
      //  ball = GameObject.FindGameObjectWithTag("Tennis");
       /* if(_distanceToCamera > distance)
        {
            hand.SetActive(false);
        }*/
    }

    private void OnMouseEnter()
    {
        // check distance from player
        if (_distanceToCamera > distance) return;
       
        if (!_isBeingDragged)
        {
            Debug.Log("Yellow Color ageen");
           // _renderer.material.color = Color.yellow;
        }
    }

    private void OnMouseOver()
    {
        // check distance from player
        if (_distanceToCamera > distance) return;
       /* else
        {
            //mouse = GameObject.Find("Hand");
            hand.SetActive(true);
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            _isBeingDragged = true;
            Debug.Log("Green Color");
           // _renderer.material.color = Color.green;
        }
    }

    private void OnMouseUp()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.useGravity = true;
        Debug.Log("Yellow Color");
        //_renderer.material.color = Color.yellow;
        _isBeingDragged = false;
        _rb.drag = 0f;
        _rb.angularDrag = 0f;
        _isBeingDragged = false;

    }

    private void OnMouseExit()
    {
        if (!_isBeingDragged)
        {
            Debug.Log("Normal Color"); 
            
            //_renderer.material.color = _objectColor;
        }
        hold.SetActive(false);
        hand.SetActive(true);
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse Down");
     //   Debug.Log("Distance to camera: " + _distanceToCamera);
        Debug.Log("Distance: " + distance);
      //  if(distance < _distanceToCamera) return;
        Debug.Log("Green Color agen");
       // _renderer.material.color = Color.green;
        _isBeingDragged = true;
    }

    private void OnMouseDrag()
    {
        hand.SetActive(false);
        hold.SetActive(true);
        
        if (!_isBeingDragged) return; 
        _isBeingDragged = true;
        _rb.useGravity = false;
        _rb.drag = 10f;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distance;
        Vector3 newPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        var position = _rb.position;
        Debug.Log(mousePosition);
        bool objectAboutToHit = Physics.Raycast(position, newPosition - position, _collider.radius*1.1f);
        if(!objectAboutToHit)
        {   
            newPosition = Vector3.Lerp(position, newPosition, 1.5f);
            _rb.MovePosition(newPosition);
        }
    }

}
