using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathFX;
    [SerializeField] Transform spawnAtRuntime;
    [SerializeField] int scorePerHit = 50;

    ScoreBoard scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void AddNonTriggerBoxCollider()
    {
        Collider enemyCollider = gameObject.AddComponent<BoxCollider>();
        enemyCollider.isTrigger = false;
    }

    // Update is called once per frame
    void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scorePerHit);
         GameObject fx= Instantiate(enemyDeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = spawnAtRuntime;
        Destroy(gameObject);
        
    }
}