using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Fx on Prefab")] [SerializeField] GameObject deathFX;
    [Tooltip("In seconds")] [SerializeField] float delayOnDeath = 2f;
    private void OnTriggerEnter(Collider other)
    {
        print("You triggered Something");
        StartDeathSequence();
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }

    void StartDeathSequence()
    {
        print("Player Dead");
        SendMessage("OnPlayerDeath");
        deathFX.SetActive(true);
        Invoke("ReloadScene", delayOnDeath);

    }
}
