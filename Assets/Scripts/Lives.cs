using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour

    
{
    [SerializeField]
    private int lives = 100;
    [SerializeField]
    public Samurai samurai;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(int damage)
    {
        lives -= damage;
        if(lives <= 0)
        {
            DisableCollider();
        }
    }

    // Function to disable the BoxCollider2D
    private void DisableCollider()
    {
        // Get the BoxCollider2D component and disable it
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            samurai.Death();
            boxCollider.enabled = false;
        }
    }
}
