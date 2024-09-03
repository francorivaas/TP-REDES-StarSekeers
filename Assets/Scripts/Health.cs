using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth = 100;

    [SerializeField]
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        if (currentHealth <= 0)
        {
            Death();
        } 
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
