using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PowerUpShield : MonoBehaviour, IPowerUp
{
    public bool enable;
    private int duration = 5;
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
            _player.GetComponent<ProgressBar>().Set(Color.blue, 2);
            StartCoroutine(TikDown(duration));
        }
    }

    private void Update()
    {
        
    }

    public IEnumerator TikDown(int time)
    {
        yield return new WaitForSeconds(time);
        
        OnTimeUp();
    }

    public void OnTimeUp()
    {
        _player.GetComponent<Health>().Shield = false;
        print("a ver");
        Destroy(this.gameObject);
    }
}