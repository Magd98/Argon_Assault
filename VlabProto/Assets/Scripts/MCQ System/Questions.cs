using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour
{
    UnityStandardAssets.Characters.FirstPerson.FirstPersonController _playercontroller;
    private bool _allSolved;
    [SerializeField] private GameObject[] animationToPlay;

    private void Start()
    {
        _allSolved = false;
        // disable all the questions
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        //enable the first question
        transform.GetChild(0).gameObject.SetActive(true);
    
        //unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //disable player movement
        _playercontroller = GameObject.FindWithTag("Player")
            .GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        _playercontroller.enabled = false;

    }
    
    public bool GetAllSolved()
    {
        return _allSolved;
    }
    public void NextQuestion(GameObject question)
    {
        Debug.Log("Next Question");
        CheckIfAllSolved();
        if (_allSolved)
        {
            // do something here when all questions are solved
            CloseQuestions();
            // turn on device
            if(transform.parent?.parent?.GetComponent<TurnOnDevices>() != null)
                transform.parent.parent.GetComponent<TurnOnDevices>().TurnOn();

            // call game manager to check if every questions in scene is solved
            // if (GameObject.FindWithTag("Game Manager"))
            // {
            //     GameManager gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
            //     gameManager.CheckAllQuestions();
            // }

            if (animationToPlay.Length > 0)
            {
                foreach (GameObject obj in animationToPlay)
                {
                    obj.BroadcastMessage("PlayAnimation");
                }
            }

            return;
        }
        int childCount = transform.childCount;
        if (childCount > 1)
        {
            int index = question.transform.GetSiblingIndex();
            Debug.Log("index: " + index);
            for (int i = (index + 1) % childCount; i < childCount; i = (i + 1) % childCount)
            {
                Debug.Log(i);
                Transform sibling = transform.GetChild(i);
                if (!sibling.GetComponent<Question>().IsSolved())
                {
                    sibling.gameObject.SetActive(true);
                    question.SetActive(false);
                    return;
                }
            }
        }
    }
    
    public void CloseQuestions()
    {
        transform.parent.gameObject.SetActive(false);
        //enable movement
        _playercontroller.enabled = true;
        // lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private bool CheckIfAllSolved()
    {
        //check that all questions are solved in children
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<Question>().IsSolved())
            {
                return false;
            }
        }
        return _allSolved = true;
        
    }
}
