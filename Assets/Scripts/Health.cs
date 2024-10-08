using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float maxHealth = 100;
    public bool NoLifes = false;
    public int Lifes = 3;

    [SerializeField]
    private float currentHealth;
    [SerializeField] private Image lifeBarFill;
    [SerializeField] private Image Borde;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        float healthPercentage = currentHealth / 100f;
        lifeBarFill.fillAmount = healthPercentage;
        if (Input.GetKeyDown(KeyCode.Space)) TakeDamage(10);
        if (currentHealth <= 0) LifesMinus();
        if (Lifes == 0) NoLifes = true;
        if (NoLifes) Death();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
    private void LifesMinus()
    {
        Lifes -= 1;
        currentHealth = maxHealth;
    }
}
