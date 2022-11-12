using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void CheckAllQuestions()
    {
        //get all game object with the tag "Question"
        GameObject[] questionsList = GameObject.FindGameObjectsWithTag("Questions");
        //loop through all the questions
        foreach (GameObject questions in questionsList)
        {
            //get the question script
            Questions questionScript = questions.GetComponent<Questions>();
            //check if the question is answered
            if (!questionScript.GetAllSolved())
            {
                //if not, return
                return;
            }
        }
        //if all questions are answered move to next scene
        Invoke(nameof(NextScene), 5f);
    }
    
    // move to next scene
    private void NextScene()
    {
        //if all questions are answered, load the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
