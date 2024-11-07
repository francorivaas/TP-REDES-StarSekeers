using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerUpInvisibility : MonoBehaviour, IPowerUp
{
    private bool enable = true;
    private int duration = 5;
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
            _player = collision.gameObject;
            if (enable && _player.GetComponent<PlayerMovement>() != null)
            {
                enable = false;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Effect(_player);
            }
   
        }
    }

    public void Effect(GameObject player)
    {
        bool enabled = false;
           if (!_player.GetPhotonView().AmOwner)
           {
               _player.GetComponent<InvisibilityEffect>().InvisibilitySwitch(false);
               StartCoroutine(TikDown(duration));
           }

        //    pv.RPC("RPC_HideShowPlayer", RpcTarget.Others, player, enabled);
    }
    
    

    [PunRPC]
public void RPC_HideShowPlayer(bool enabled)
{
    Debug.Log("RPC");
    _player.GetComponent<SpriteRenderer>().enabled = enabled;
}

    public IEnumerator TikDown(int time)
    {
        yield return new WaitForSeconds(time);
        OnTimeUp();
    }

    public void OnTimeUp()
    {
        bool enabled = false;
        _player.GetComponent<InvisibilityEffect>().InvisibilitySwitch(true);
//        pv.RPC("RPC_HideShowPlayer", RpcTarget.Others, _player, enabled);
        Destroy(this.gameObject);
    }
}