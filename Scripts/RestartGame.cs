using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class RestartGame : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    [SerializeField] string videoFileName;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        PlayVideo();
        
        // Subscribe to the videoPlayer's loopPointReached event
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Method to be called when the video ends
    void OnVideoEnd(VideoPlayer vp)
    {
        // Unsubscribe from the event to prevent multiple calls
        vp.loopPointReached -= OnVideoEnd;

        // Load the next scene
        LoadNextScene();
    }

    // Method to load the next scene
    void LoadNextScene()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if(videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }


    }
    
}
