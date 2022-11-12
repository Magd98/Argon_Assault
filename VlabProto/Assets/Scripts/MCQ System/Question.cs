using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Question : MonoBehaviour
{
    [SerializeField] private string question;
    //create a list of 4 answers
    [SerializeField] private List<string> choices;
    [SerializeField] private int correctChoice;

    private bool m_Solved;
    

    private void Start()
    {
        m_Solved = false;
        //set question text
        transform.Find("QuestionText").GetComponent<TextMeshProUGUI>().text = question;
        //set choices text
        Transform choicesObject = transform.Find("Choices");
        Debug.Log(choicesObject.childCount);
        for (int i = 0; i < choices.Count; i++)
        {
            if (choicesObject.GetChild(i))
            {
                choicesObject.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = choices[i];
            }
        }
    }
    
    // isSolved
    public bool IsSolved()
    {
        return m_Solved;
    }
    public bool CheckAnswer(string choice)
    {
        if(choice == choices[correctChoice])
        {
            m_Solved = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
