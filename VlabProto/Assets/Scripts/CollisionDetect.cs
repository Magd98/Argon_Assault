using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private int _colliosnCount = 0;
    private void OnCollisionEnter(Collision collision)
    {
        _colliosnCount++;
        Debug.Log(_colliosnCount);
    }
}
