using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{

    [SerializeField] private AudioSource finishSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //main character animator
        Animator playerAnimator = collision.gameObject.GetComponent<Animator>();


        playerAnimator.SetTrigger("finishedLevel");
        finishSoundEffect.Play();
        //moves on after 2 seconds
        Invoke("LoadFinalScene", 4f);
        
    }

    private void LoadFinalScene() 
    {
        int potions = PlayerPrefs.GetInt("Potions", 0);

            if(potions >= 6)
            {
                //you win scene
                Debug.Log("win scene with " + potions + " potions");
                SceneManager.LoadScene("WinScene");
            }
            else{
                //game over scene
                SceneManager.LoadScene("LoseScene");
            }

    }





}
