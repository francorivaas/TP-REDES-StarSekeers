using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;

public class ShootController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePosition;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public bool doubleShoot = false;
    private float timer;
    public float timeBetweenFire;
    private SpriteRenderer spriteRenderer;
    private PhotonView pv;
    
    public bool DoubleShoot { get => doubleShoot; set { doubleShoot = value; } }
    public float TimeBetweenFire { get => timeBetweenFire; set { timeBetweenFire = value; } }

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }
    private void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFire )
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire && pv.IsMine)
        {
            canFire = false;
            if (doubleShoot)
            {
                // Aca debería instanciar dos balas, y rotar una 45° y la otra -45°
                GameObject _bullet = PhotonNetwork.Instantiate(bullet.name, bulletTransform.position, Quaternion.identity);
                _bullet.transform.Rotate(0,0,45);
            }
            else
            {
                PhotonNetwork.Instantiate(bullet.name, bulletTransform.position, Quaternion.identity);
            }
            
        }
    }
}
