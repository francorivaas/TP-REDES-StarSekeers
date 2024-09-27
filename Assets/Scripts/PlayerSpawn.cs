using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject player;
    private PhotonView pv;

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
        pv.RPC("ChangeColor", RpcTarget.AllBuffered, player.GetComponent<PhotonView>().ViewID, playerIndex);

        if (PhotonNetwork.PlayerList.Length != 2)
        {
            StartCoroutine(TimerToPlay());
        }
        else
        {
         StopCoroutine(TimerToPlay());
        }
    }
    [PunRPC]
    private void ChangeColor(int playerViewID, int playerIndex)
    {
        PhotonView targetPhotonView = PhotonView.Find(playerViewID);
        if (targetPhotonView != null) targetPhotonView.gameObject.GetComponent<SpriteRenderer>().color = (playerIndex == 1) ? Color.red : Color.blue;
    }

    private IEnumerator TimerToPlay()
    {
        yield return new WaitForSeconds(60);
        
        // Show Error PopUp
    }
}
