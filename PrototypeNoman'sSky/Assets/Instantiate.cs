using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cube;
    public void Alive()
    {
       
        Instantiate(cube,transform.position,transform.rotation);
    }
}