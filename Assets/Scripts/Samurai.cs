using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    private bool canMove = true; // Flag to determine if the object can move
    private Animator animator; // Reference to the Animator component
    private bool canAttack = true;
    private bool dead = false;
    Lives damageReceiver;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // You can call the Move function from other parts of your code or events
        if (canMove)
        {
            Move();
        }
        else
        {
            Attack();
        }
    }

    IEnumerator Attack()
    {
        while (!canMove)
        {
            yield return new WaitForSeconds(2f);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.8f);
            if (damageReceiver != null)
            {
                // Call the GetDamage function on the collided object
                damageReceiver.GetDamage(10); // You can pass the desired damage value
            }
        }
    }

    // Called when another collider enters the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Set canMove to false when a collider enters the trigger
        canMove = false;
        Debug.Log("Triggered");
        // Set the "Enemy" parameter to true
        animator.SetBool("Enemy", true);
        damageReceiver = other.GetComponent<Lives>();
        StartCoroutine(Attack());
    }

    // Called when another collider exits the trigger collider
    void OnTriggerExit2D(Collider2D other)
    {
        if (!dead)
        {
            // Set the "Enemy" parameter to false
            animator.SetBool("Enemy", false);
            // Set canMove to true when a collider exits the trigger
            canMove = true;
        }
        
    }

    // Function to move the game object towards the right in the X-axis
    void Move()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    public void Death()
    {
        dead = true;
        animator.SetTrigger("Dead");
        StartCoroutine(destroy(2f));   
    }

    IEnumerator destroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}