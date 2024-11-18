using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanController : MonoBehaviour
{
    public float damage;

    private void Start()
    {
        damage = Random.Range(5, 10);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
