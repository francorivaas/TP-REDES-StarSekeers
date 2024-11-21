using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PowerUpDoubleShoot : MonoBehaviour, IPowerUp
{
    private bool enable = true;
    private int duration = 5;
    private ShootController _player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (enable)
        {
            if (collision.GetComponentInChildren<ShootController>() != null)
            {
                print("colisiona double shoot");
                _player = collision.gameObject.GetComponentInChildren<ShootController>();
                Effect(_player.gameObject);
                enable = false;
            }

        }
    }

    public void Effect(GameObject player)
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _player.doubleShoot = true;
            _player.timeBetweenFire = _player.GetComponent<ShootController>().timeBetweenFire * 1.5f;
            _player.GetComponentInParent<ProgressBar>().Set(Color.red, 0);
            StartCoroutine(TikDown(duration));
    }

    public IEnumerator TikDown(int time)
    {
        yield return new WaitForSeconds(time);
        
        OnTimeUp();
    }

    public void OnTimeUp()
    {
        _player.GetComponent<ShootController>().doubleShoot = false;
        _player.GetComponent<ShootController>().timeBetweenFire = _player.GetComponent<ShootController>().timeBetweenFire / 1.5f;

        Destroy(this.gameObject);
    }
}