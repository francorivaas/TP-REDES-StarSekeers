using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    private float lifetime = 30f;
    private float currentLifetime = 0f;

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        currentLifetime += Time.deltaTime;
        if (currentLifetime >= lifetime)
        {
            DestroyObstacle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            DestroyObstacle();
        }
    }

    public void DestroyObstacle()
    {
        Destroy(gameObject);
    }
}
