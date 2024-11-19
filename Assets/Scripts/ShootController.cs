using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ShootController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePosition;
    public GameObject bullet;
    public GameObject specialBullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFire;
    private SpriteRenderer spriteRenderer;
    private PhotonView pv;
    public PowerBar powerBar;
    public bool doubleShoot = false;

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
                // Instanciamos la bala y obtenemos la referencia a su componente BulletController
                GameObject newBullet = PhotonNetwork.Instantiate(bullet.name, bulletTransform.position, Quaternion.identity);
                BulletController bulletController = newBullet.GetComponent<BulletController>();
                bulletController.InitializeBullet(pv.Owner.ActorNumber); // Pasamos el ID del jugador que dispara
                bulletController.DegreesToRotate = 3;
                
                // Instanciamos la bala y obtenemos la referencia a su componente BulletController
                GameObject SecondBullet = PhotonNetwork.Instantiate(bullet.name, bulletTransform.position, Quaternion.identity);
                BulletController SecondbulletController = SecondBullet.GetComponent<BulletController>();
                SecondbulletController.InitializeBullet(pv.Owner.ActorNumber); // Pasamos el ID del jugador que dispara
                SecondbulletController.DegreesToRotate = -3;
            }
            else
            {
                // Instanciamos la bala y obtenemos la referencia a su componente BulletController
                GameObject newBullet = PhotonNetwork.Instantiate(bullet.name, bulletTransform.position, Quaternion.identity);
                BulletController bulletController = newBullet.GetComponent<BulletController>();
                bulletController.InitializeBullet(pv.Owner.ActorNumber); // Pasamos el ID del jugador que dispara
            }
        }
        if (Input.GetMouseButton(1) && powerBar.IsPowerReady() && pv.IsMine)
        {
            GameObject newSpecialBullet = PhotonNetwork.Instantiate(specialBullet.name, bulletTransform.position, Quaternion.identity);
            BulletController specialBulletController = newSpecialBullet.GetComponent<BulletController>();
            specialBulletController.InitializeBullet(pv.Owner.ActorNumber);
            powerBar.ResetPower();
        }
    }
}
