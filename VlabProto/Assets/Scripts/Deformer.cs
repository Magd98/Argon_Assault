using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deformer : MonoBehaviour
{

    public DeformableMesh deformableMesh;
    private float mass;
    public float collisionRadius = 0.1f;
    public Rigidbody rb;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter (Collision targetObj) {
    
        rb = targetObj.gameObject.GetComponent<Rigidbody>();
        mass = rb.mass / 2;
        velocity = targetObj.relativeVelocity.magnitude / 2;
        collisionRadius = 0.1f * mass * velocity;
        Debug.Log("velocity =" + velocity );

    }
    void OnCollisionStay(Collision collision){

        foreach (var contact in collision.contacts)
            {
                deformableMesh.AddDepression(contact.point, collisionRadius);
                
            }

    }
    

}
