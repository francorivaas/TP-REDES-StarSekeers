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
            PhotonNetwork.Instantiate(bullet.name, bulletTransform.position, Quaternion.identity);
        }
        if (Input.GetMouseButton(1) && powerBar.IsPowerReady() && pv.IsMine)
        {
            PhotonNetwork.Instantiate(specialBullet.name, bulletTransform.position, Quaternion.identity);
            powerBar.ResetPower();
        }
    }
}
