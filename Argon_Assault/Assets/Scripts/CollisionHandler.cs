using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("You triggered Something");
        StartDeathSequence();
    }

    void StartDeathSequence()
    {
        print("Player Dead");
        SendMessage("OnPlayerDeath");
        
    }
}
