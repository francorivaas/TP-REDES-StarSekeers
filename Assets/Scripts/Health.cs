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
    [SerializeField] private Image Rombo1;
    [SerializeField] private Image Rombo2;
    [SerializeField] private Image Rombo3;
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
        if (Lifes == 2) Rombo3.gameObject.SetActive(false);
        if (Lifes == 1) Rombo2.gameObject.SetActive(false);
        if (Lifes == 0) 
        {
            NoLifes = true;
            Rombo1.gameObject.SetActive(false);
        }
        if (NoLifes) Death();
        Debug.Log(currentHealth);
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
