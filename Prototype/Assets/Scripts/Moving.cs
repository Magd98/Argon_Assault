using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;


public class Moving : MonoBehaviour
{
    [SerializeField] GameObject objectToPop;
    [SerializeField] GameObject objectToDestroy; 
    [SerializeField] float speed = 10f;
    AudioSource audioSource;
    [SerializeField] VideoPlayer videoOn;
    [SerializeField] TMP_Text equationToPreview;
    [SerializeField] float timeForEquationToDisappear;
    private float beginning;
    enum State { inMotion, Froze }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.inMotion;
        beginning = Time.time;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.inMotion)
        {
            Movement();

        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, 0f, speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.S))
            transform.Translate(0f, 0f, -speed * Time.deltaTime);

        else if (Input.GetKey(KeyCode.A))
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);

        else if (Input.GetKey(KeyCode.D))
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FirstSceneVideo")
        {
            objectToPop.gameObject.SetActive(true);
            equationToPreview = FindObjectOfType<TMP_Text>();
            videoOn = FindObjectOfType<VideoPlayer>();
            videoOn.Play();
            audioSource.Pause();
            StartCoroutine(WaitForVideo());
        }

        IEnumerator WaitForVideo()
        {
            while (Time.time - beginning < videoOn.length)
            {
                state = State.Froze;
                yield return null;
            }
            DestroyObject();
            StartCoroutine(UIPop());
            
        }
    }

        IEnumerator UIPop()
    {
        float timeNow = 0f;
        while( timeNow < timeForEquationToDisappear)
        {
            equationToPreview.SetText("Kinetic Energy=1/2 MV^2");
            timeNow += Time.deltaTime;
            yield return null;
        }

        equationToPreview.SetText("");
        
    }

    void DestroyObject()
    {
        state = State.inMotion;
        audioSource.UnPause();
        Destroy(objectToDestroy);
    }
}

   

