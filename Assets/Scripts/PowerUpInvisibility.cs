using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerUpInvisibility : MonoBehaviour, IPowerUp
{
    private bool enable = true;
    private int duration = 1;
    private GameObject _player;
    private PhotonView pv;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (enable)
        {
            if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            {
                _player = collision.gameObject;
                print("colisiono invisibilidad");
                Effect(collision.gameObject);
            }
   
        }
    }

    public void Effect(GameObject player)
    {
        enabled = false;
        Debug.Log("Call RPC");
        pv.RPC("RPC_HideShowPlayer", RpcTarget.Others, player);
 //       if (!_player.GetPhotonView().AmOwner)
        {
            StartCoroutine(TikDown(duration));
 //           pv.RPC("RPC_HideShowPlayer", RpcTarget.Others, player, true);
          //  _player.GetComponent<InvisibilityEffect>().InvisibilitySwitch(false);
        }

//           pv.RPC("RPC_HideShowPlayer", RpcTarget.Others, player, enabled);
    }
    
    

    [PunRPC] 
    public void RPC_HideShowPlayer(GameObject player)
{
    Debug.Log("RPC");
    player.GetComponent<InvisibilityEffect>().InvisibilitySwitch(false);
}

    public IEnumerator TikDown(int time)
    {
        print("llamo al tik");
        yield return new WaitForSeconds(time);
        OnTimeUp();
    }

    public void OnTimeUp()
    {
        print("on time up");
        enabled = false;
        _player.GetComponent<InvisibilityEffect>().InvisibilitySwitch(true);
//        pv.RPC("RPC_HideShowPlayer", RpcTarget.Others, _player, enabled);
        Destroy(this.gameObject);
    }
}