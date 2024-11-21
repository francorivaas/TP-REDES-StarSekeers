using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public Sprite[] playerSprites;
    private GameObject player;
    private PhotonView pv;
    private bool HasToCheckForSecondPlayer;
    [SerializeField] private float TimeToConnect = 60f;
    private float CurrentTimeWaiting = 0f;
    [SerializeField] private GameObject ErrorPopup;
    [SerializeField] private GameObject PopUpButton;
    [SerializeField] private TextMeshProUGUI ErrorTxt;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name, 
            new Vector2(Random.Range(-4, 4), 
            Random.Range(-4, 4)), Quaternion.identity);
        int playerIndex = PhotonNetwork.PlayerList.Length;
        pv.RPC("ChangeSprite", RpcTarget.AllBuffered, player.GetComponent<PhotonView>().ViewID, playerIndex);
        if (PhotonNetwork.PlayerList.Length == 1) HasToCheckForSecondPlayer = true;

    }
    [PunRPC]
    private void ChangeSprite(int playerViewID, int playerIndex)
    {
        PhotonView targetPhotonView = PhotonView.Find(playerViewID);
        if (targetPhotonView != null) 
        {
            SpriteRenderer spriteRenderer = targetPhotonView.gameObject.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null && playerIndex > 0 && playerIndex <= playerSprites.Length) spriteRenderer.sprite = playerSprites[playerIndex - 1];
        }
    }

    private void Update()
    {
        if (HasToCheckForSecondPlayer)
        {
            if (PhotonNetwork.PlayerList.Length == 2)
            {
                HasToCheckForSecondPlayer = false;
                CurrentTimeWaiting = 0;
            }
            else
            {
                CurrentTimeWaiting += Time.deltaTime;
                if (CurrentTimeWaiting >= TimeToConnect)
                {
                    ErrorPopup.SetActive(true);
                    Time.timeScale = 0;
                    PopUpButton.SetActive(true);
                    ErrorTxt.text = "Se agoto el tiempo de espera por un segundo jugador";
                }
   
            }
        }
    }
}
