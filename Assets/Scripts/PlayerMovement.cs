using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movSpeed;
    private float speedX, speedY;
    private Rigidbody2D rb;
    private PhotonView pv;

    [Header("Turbo Settings")]
    public float turboMultiplier = 2f;
    public float turboDuration = 3f;
    public float turboCooldown = 5f;

    private float turboTimer;
    private float cooldownTimer;
    private bool isTurboActive;
    private bool canUseTurbo = true;

    [Header("UI Settings")]
    public Slider turboSlider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        turboSlider.maxValue = turboDuration;
        turboSlider.value = turboDuration;
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            HandleMovement();
            HandleTurbo();
            UpdateTurboSlider();
        }
    }

    private void HandleMovement()
    {
        float currentSpeed = movSpeed;

        if (isTurboActive)
        {
            currentSpeed *= turboMultiplier;
        }

        speedX = Input.GetAxisRaw("Horizontal") * currentSpeed;
        speedY = Input.GetAxisRaw("Vertical") * currentSpeed;
        rb.velocity = new Vector2(speedX, speedY);
    }

    private void HandleTurbo()
    {
        if (canUseTurbo && Input.GetKeyDown(KeyCode.LeftShift))
        {
            ActivateTurbo();
        }

        if (isTurboActive)
        {
            turboTimer -= Time.deltaTime;
            if (turboTimer <= 0)
            {
                DeactivateTurbo();
            }
        }
        else if (!canUseTurbo)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                canUseTurbo = true;
                turboSlider.value = turboDuration;
            }
        }
    }

    private void ActivateTurbo()
    {
        isTurboActive = true;
        canUseTurbo = false;
        turboTimer = turboDuration;
        cooldownTimer = turboCooldown;
    }

    private void DeactivateTurbo()
    {
        isTurboActive = false;
    }

    private void UpdateTurboSlider()
    {
        if (isTurboActive)
        {
            turboSlider.value = turboTimer;
        }
        else if (!canUseTurbo)
        {
            float fillAmount = (turboCooldown - cooldownTimer) / turboCooldown;
            turboSlider.value = fillAmount * turboDuration;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pv.IsMine && collision.transform.CompareTag("Coin"))
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("CollectCoin", RpcTarget.AllBuffered, collision.gameObject.GetComponent<PhotonView>().ViewID);
        }
    }

    [PunRPC]
    void CollectCoin(int coinViewID)
    {
        PhotonView coinPhotonView = PhotonView.Find(coinViewID);
        if (coinPhotonView != null)
        {
            PhotonNetwork.Destroy(coinPhotonView.gameObject);
            GameManager.instance.AddCoinToPool();
        }
    }
}


