using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Health : MonoBehaviourPunCallbacks
{
    private float maxHealth = 100;
    public bool NoLifes = false;
    public int Lifes = 3;
    private bool shield = false;
    
    public bool Shield { get => shield; set { shield = value; } }

    [SerializeField]
    private float currentHealth;
    [SerializeField] private Image lifeBarFill;
    [SerializeField] private Image Borde;
    [SerializeField] private Image Rombo1;
    [SerializeField] private Image Rombo2;
    [SerializeField] private Image Rombo3;
    private PhotonView pv;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        float healthPercentage = currentHealth / 100f;
        lifeBarFill.fillAmount = healthPercentage;
        if (Input.GetKeyDown(KeyCode.Space) && pv.IsMine) TakeDamage(10);
        if (currentHealth <= 0 && pv.IsMine) LifesMinus();
        if (Lifes == 2) Rombo3.gameObject.SetActive(false);
        if (Lifes == 1) Rombo2.gameObject.SetActive(false);
        if (Lifes == 0)
        {
            NoLifes = true;
            Rombo1.gameObject.SetActive(false);
        }
        if (NoLifes && pv.IsMine) Death();
    }

    public void TakeDamage(float damage)
    {
        if (!pv.IsMine) return;

        if (shield) return;
        //Sincronizamos el da�o con los dem�s jugadores usando un RPC
        pv.RPC("RPC_TakeDamage", RpcTarget.AllBuffered, damage);
    }
    [PunRPC]
    public void RPC_TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    private void Death()
    {
        if (!pv.IsMine) return;
        pv.RPC("RPC_Death", RpcTarget.AllBuffered);
    }
    [PunRPC]
    private void RPC_Death()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    private void LifesMinus()
    {
        if (!pv.IsMine) return;
        //Sincronizamos la p�rdidad de vida y reseteamos la salud
        pv.RPC("RPC_LifesMinus", RpcTarget.AllBuffered);
        
    }
    [PunRPC]
    private void RPC_LifesMinus()
    {
        Lifes -= 1;
        currentHealth = maxHealth;
    }

    public void HealUp()
    {
        if (!pv.IsMine) return;
        
        pv.RPC("RPC_Heal", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void RPC_Heal()
    {
        currentHealth = maxHealth;
        Lifes = 3;
    }
}
