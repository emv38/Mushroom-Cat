using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;

    private Animator slugAnimator;

    [SerializeField] private AudioSource slugDeathSoundEffect;

    [SerializeField] private AudioSource catDeathSoundEffect;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("enemy"))
        {
            if (IsAbove(other.transform))
            {
                // Main character jumped on the slug, destroy the slug
                Debug.Log("Destroying slug");
                DestroySlug(other.gameObject);
            }
            
            else
            {
                // Slug collided with the main character from the side or below
                Debug.Log("Main character died");
                Die();
            }
        }
    }

    bool IsAbove(Transform otherTransform)
    {
        // Check if the main character is above the other object
        return transform.position.y > otherTransform.position.y;
    }

   private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("trap"))
        {
           Die();
        }
        if(collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("touched the slug");
            if(IsAbove(collision.transform) == false)
            {
                Die();
            }
        }
    }

    void DestroySlug(GameObject slug)
    {
        slugAnimator = slug.GetComponent<Animator>();
        Rigidbody2D slugRigidbody = slug.GetComponent<Rigidbody2D>();
        slugRigidbody.bodyType = RigidbodyType2D.Static;
        //play the slug death sound
        slugDeathSoundEffect.Play();
        //start the animation
        slugAnimator.SetTrigger("slug_death");
        // Destroy the slug GameObject
        
    }
    

    private void Die() 
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        catDeathSoundEffect.Play();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
