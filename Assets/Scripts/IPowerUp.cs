using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp
{
    void OnTriggerEnter2D(Collider2D collision);
    void Effect(GameObject player);
    IEnumerator TikDown(int time);
    void OnTimeUp();
}
