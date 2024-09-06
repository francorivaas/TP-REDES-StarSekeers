using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

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
        Destroy(gameObject, 0.2f);

        if (animator != null) animator.SetBool("Destroy", true);
    }
}
