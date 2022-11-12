using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimationtrigger : MonoBehaviour
{
    [SerializeField] private CompareType compareType;
    enum CompareType
    {
        Speed,
        Mass,
    }
    
    public void PlayAnimation()
    {
        if (compareType == CompareType.Speed)
        {
            GetComponent<Animator>().Play("CarAnimation1");
            GetComponent<Animator>().Play("CarAnimation2");
        }
        else if (compareType == CompareType.Mass)
        {
            GetComponent<Animator>().Play("CarAnimation3");
            GetComponent<Animator>().Play("VanAnimation1");
        }
    }
}
