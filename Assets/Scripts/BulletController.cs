using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera mainCamera;
    private Rigidbody2D body;
    public float force;
    private float lifeTime = 3;
    
    [SerializeField]
    private float damage;

    [SerializeField] private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        body = GetComponent<Rigidbody2D>();
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        body.velocity = new Vector2 (direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2 (rotation.x, rotation.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            scoreManager.AddScore("Enemy", 1);
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            scoreManager.AddScore("Obstacle", 1);
            Destroy(gameObject);
        }
    }
}
