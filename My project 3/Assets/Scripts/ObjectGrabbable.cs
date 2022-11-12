using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    [SerializeField] private float lerpTime = 2f;
    private Rigidbody _rb;
    private Transform _grabPoint;
    private Color _originalColor;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _originalColor = GetComponent<Renderer>().material.color;
    }

    private void OnMouseOver()
    {
        if (_grabPoint == null && Vector3.Distance(transform.position, Camera.main.transform.position) < 3f)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }    
    }

    private void OnMouseExit()
    {
        if (_grabPoint == null)
        {
            GetComponent<Renderer>().material.color = _originalColor;
        }
    }


    public void Grab(Transform grabPoint)
    {
        _grabPoint = grabPoint;
        _rb.useGravity = false;
        _rb.drag = 99f;
        GetComponent<Renderer>().material.color = _originalColor;


    }

    public void Drop()
    {
        this._grabPoint = null;
        _rb.useGravity = true;
        _rb.drag = 0f;
    }

    private void FixedUpdate()
    {
        if (_grabPoint != null) 
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, _grabPoint.position, lerpTime * Time.deltaTime);
            if (!Physics.Raycast(_rb.position, newPosition - _rb.position,
                    GetComponent<SphereCollider>().radius * 1.2f))
            {
                _rb.MovePosition(newPosition);
            }
            
        }

    }
}
