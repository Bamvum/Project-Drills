using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LobbySceneFlow : MonoBehaviour
{
    [Header("Scene Start")]
    [SerializeField] private GameObject startPanel;

    [Header("Television")]
    [SerializeField] private VideoClip videoClip1;
    [SerializeField] private VideoClip videoClip2;
    [SerializeField] private VideoClip videoClip3;
    [SerializeField] private VideoClip videoClip4;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private int currentVideoIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        startPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractTelevision()
    {
        // Check for "E" key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Toggle between videos or stop the TV
            if (currentVideoIndex == 0)
            {
                currentVideoIndex = 1;
            }
            else if (currentVideoIndex == 1)
            {
                currentVideoIndex = 2;
            }
            else if (currentVideoIndex == 2)
            {
                currentVideoIndex = 3; 
            } 
            else if (currentVideoIndex == 3)
            {
                currentVideoIndex = -1; // Stop playing on the 4th press
            }
            else
            {
                // On subsequent presses, reset to index 0
                currentVideoIndex = 0;
            }

            // Play the corresponding video or stop the video player
            PlayVideo();
        }
    } 

    void PlayVideo()
    {
        if (currentVideoIndex == 0)
        {
            videoPlayer.clip = videoClip1;
            videoPlayer.Play();
        }
        else if (currentVideoIndex == 1)
        {
            videoPlayer.clip = videoClip2;
            videoPlayer.Play();
        }
        else if (currentVideoIndex == 2)
        {
            videoPlayer.clip = videoClip3;
            videoPlayer.Play();
        }
        else if (currentVideoIndex == 3)
        {
            videoPlayer.clip = videoClip4;
            videoPlayer.Play();
        }
        else
        {
            // Stop the video player if turning off the TV
            videoPlayer.Stop();
        }
    }
}
