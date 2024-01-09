using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioSource startAudio;

    public void StartGame()
    {
        
        StartCoroutine(PlayAudioAndLoadScene());
    }

    IEnumerator PlayAudioAndLoadScene()
    {
        // Play the audio
        startAudio.Play();

        
        yield return new WaitForSeconds(2f);

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
