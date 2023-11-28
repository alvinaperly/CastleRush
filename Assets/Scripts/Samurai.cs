using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : MonoBehaviour
{
    [SerializeField]
    private int faction = 0;
    [SerializeField]
    public float attackSpeed = 1f;
    [SerializeField]
    private float moveSpeed = 5f; // Adjust the speed as needed
    private bool canMove = true; // Flag to determine if the object can move
    private Animator animator; // Reference to the Animator component
    private bool canAttack = true;
    private bool dead = false;
    Lives damageReceiver;
    [SerializeField]
    private int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (faction == 2)
        {
            RotateObjectBy180Degrees();
        }
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }

    void RotateObjectBy180Degrees()
    {
        // Get the current rotation
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Set the new rotation by adding 180 degrees to the Y-axis
        Vector3 newRotation = new Vector3(currentRotation.x, currentRotation.y + 180f, currentRotation.z);

        // Apply the new rotation to the object
        transform.rotation = Quaternion.Euler(newRotation);
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
            yield return new WaitForSeconds(attackSpeed);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.8f);
            if (damageReceiver != null)
            {
                // Call the GetDamage function on the collided object
                damageReceiver.GetDamage(damage); // You can pass the desired damage value
            }
        }
    }

    // Called when another collider enters the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Set canMove to false when a collider enters the trigger
        canMove = false;

        // Set the "Enemy" parameter to true
        animator.SetBool("Enemy", true);

        Samurai samurai = other.GetComponent<Samurai>();
        if (samurai.getFaction() == faction)
        {
            if(samurai.getDamageReceiver() != null)
            {
                damageReceiver = samurai.getDamageReceiver();
            }
        }
        else
        {
            damageReceiver = other.GetComponent<Lives>();
        }
        StartCoroutine(Attack());

    }

    // Called when another collider exits the trigger collider
    void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(TriggerExir(0.5f));
    }

    IEnumerator TriggerExir(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!dead)
        {
            damageReceiver = null;
            // Set the "Enemy" parameter to false
            animator.SetBool("Enemy", false);
            // Set canMove to true when a collider exits the trigger
            canMove = true;
        }
    }

    public Lives getDamageReceiver()
    {
        return damageReceiver;
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

    public int getFaction()
    {
        return faction;
    }
}