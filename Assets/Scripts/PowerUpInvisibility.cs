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
        Debug.Log("Call RPC");
        enabled = false;
        Color orange = new Color(1.0f, 0.64f, 0.0f, 255);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        _player = player;
        _player.GetComponent<InvisibilityEffect>().Called(false);
        StartCoroutine(TikDown(duration));
        _player.GetComponent<ProgressBar>().Set(orange, 1);
    }

    public IEnumerator TikDown(int time)
    {
        print("llamo al tikDown");
        yield return new WaitForSeconds(time);
        OnTimeUp();
    }

    public void OnTimeUp()
    {
        print("on time up");
        _player.GetComponent<InvisibilityEffect>().Called(true);
        Destroy(this.gameObject);
    }
}