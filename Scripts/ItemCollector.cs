using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    //private int potions = 0;
    [SerializeField] private TMP_Text potionsText;
    Animator potion_Animator;

    private bool potionCollected = false;

    [SerializeField] private AudioSource collectPotionSoundEffect;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("potion") && !potionCollected)
        {

            potionCollected = true;

            int potions = PlayerPrefs.GetInt("Potions", 0);

            potion_Animator = collision.gameObject.GetComponent<Animator>();
            potion_Animator.SetBool("collected" , true);

            collectPotionSoundEffect.Play();
            potions++;
            Destroy(collision.gameObject , .4f);


            
            PlayerPrefs.SetInt("Potions", potions);

            potionsText.text = "Potions: " + potions;
            
            Debug.Log("potions: " + potions);

            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("potion"))
        {
            potionCollected = false;
        }
    }
    
}
