using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOnDevices : MonoBehaviour
{
    [SerializeField] private GameObject deviceDisplay;
    private bool isActive;

    private void Start()
    {
        TurnOff();
       
    }
    private void TurnOff()
    {
        deviceDisplay.SetActive(false);
        CheckAnimation();
    }

    public void TurnOn()
    {
        CheckAnimation();
        deviceDisplay.SetActive(true);
        Invoke("TurnOff", 10f);
    }

    private void CheckAnimation()
    {
        if (transform.GetComponent<Animator>())
        {
            transform.GetComponent<Animator>().enabled = !transform.GetComponent<Animator>().enabled;

        }
        
    }
}