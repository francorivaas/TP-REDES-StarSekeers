using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PowerUpShield : MonoBehaviour, IPowerUp
{
    private bool enable = true;
    private int duration = 500;
    private GameObject _player;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (enable)
        {
            _player = collision.gameObject;
            Health playerHealth = _player.GetComponent<Health>();
            Debug.Log(playerHealth);
            if (playerHealth != null)
            {
                print("colisiona shield");
                Effect(_player);
                enable = false;
            }
            
        }
    }

    public void Effect(GameObject player)
    {
        
//        if (!_player.GetPhotonView().AmOwner)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _player.GetComponent<Health>().Shield = true;
            StartCoroutine(TikDown(duration));
        }
    }

    public IEnumerator TikDown(int time)
    {
        yield return new WaitForSeconds(time);
        
        OnTimeUp();
    }

    public void OnTimeUp()
    {
        _player.GetComponent<Health>().Shield = false;
        Destroy(this.gameObject);
    }
}