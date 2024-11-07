using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletController : MonoBehaviour
{
    public bool isSpecialBullet = false;
    private Vector3 mousePosition;
    private Camera mainCamera;
    private Rigidbody2D body;
    public float force;
    private float lifeTime = 3;
    
    [SerializeField]
    private float damage;
    [SerializeField]
    private float specialDamageMultiplier = 2f;
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
        if (isSpecialBullet) damage *= specialDamageMultiplier;
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
            scoreManager.AddScore("Player", 1);

            if (PhotonNetwork.IsMasterClient) 
                PhotonNetwork.Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            scoreManager.AddScore("Obstacle", 1);
            collision.gameObject.GetComponent<Obstacle>().DestroyObstacle();
            
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);
        }
    }
}
