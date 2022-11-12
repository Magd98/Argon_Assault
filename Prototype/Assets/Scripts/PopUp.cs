using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PopUp : MonoBehaviour
{
    [SerializeField] GameObject objectToPop;
    VideoPlayer videoPlayer;
    double  videoLength;
    // Start is called before the first frame update

     void Start()
    {
       // videoPlayer = GetComponent<VideoPlayer>();
        //videoLength = videoPlayer.length;
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cube")
        {
            objectToPop.gameObject.SetActive(true);
            
              
       }
        

        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == "Cube")
            {
                objectToPop.gameObject.SetActive(false);
            }
        }
    }
