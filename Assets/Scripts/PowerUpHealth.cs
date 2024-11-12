using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : MonoBehaviour, IPowerUp
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Effect(collision.gameObject);
        
    }

    public void Effect(GameObject player)
    {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            print("colisiona health");
            playerHealth.HealUp();
            Destroy(this.gameObject);
        }
    }

    public IEnumerator TikDown(int time)
    {
        yield return new WaitForSeconds(time);
    }

    public void OnTimeUp()
    {
    }
}
