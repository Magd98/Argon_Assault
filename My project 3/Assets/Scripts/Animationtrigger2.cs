using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationtrigger2 : MonoBehaviour
{
    enum CompareType
    {
        Speed,
        Mass,
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){

            GetComponent<Animator>().Play("CarAnimation3");
            GetComponent<Animator>().Play("VanAnimation1");
        }
    }
}
