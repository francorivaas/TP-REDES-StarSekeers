using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movSpeed;
    private float speedX, speedY;
    private Rigidbody2D rb;
    private PhotonView pv;

    [Header("Turbo Settings")]
    public float turboMultiplier = 2f; // Multiplicador de velocidad al usar turbo
    public float turboDuration = 3f;   // Duración del turbo en segundos
    public float turboCooldown = 5f;   // Tiempo de recarga del turbo en segundos

    private float turboTimer;
    private float cooldownTimer;
    private bool isTurboActive;
    private bool canUseTurbo = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            HandleMovement();
            HandleTurbo();
        }
    }

    private void HandleMovement()
    {
        float currentSpeed = movSpeed;

        // Si el turbo está activo, incrementar la velocidad
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
        // Activar turbo al presionar Shift si está disponible
        if (canUseTurbo && Input.GetKeyDown(KeyCode.LeftShift))
        {
            ActivateTurbo();
        }

        // Si el turbo está activo, decrementar el temporizador
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
                canUseTurbo = true; // Recargar turbo
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

