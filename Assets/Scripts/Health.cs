using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float maxHealth = 100;

    [SerializeField]
    private float currentHealth;

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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
