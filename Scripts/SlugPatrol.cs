using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SlugPatrol : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    public float speed;

    public float rayLength;

    private bool moveLeft;
    
    public Transform contactChecker;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveLeft ? -speed : speed, rb.velocity.y);

    }

    private void FixedUpdate()
    {
        RaycastHit2D contactCheck = Physics2D.Raycast(contactChecker.position, moveLeft ? Vector2.left : Vector2.right, rayLength);

        if (contactCheck.collider == null)
        {
            // No obstacle detected, continue moving
            return;
        }

        // Obstacle detected, change direction
        Flip();
    }

    private void Flip()
    {
        moveLeft = !moveLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("stomper"))
        {
            Debug.Log("DESTROY SLUG");
            Die();
            //Destroy(gameObject);
        }
    }
    */
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("slug_death");
        //DestroySlug();
    }

    private void DestroySlug()
    {
        Debug.Log("now destroying object");
        Destroy(gameObject);
    }
}
