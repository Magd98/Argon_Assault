using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Invoke("LoadNextScene", 4f);
    }

    // Update is called once per frame
   void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
