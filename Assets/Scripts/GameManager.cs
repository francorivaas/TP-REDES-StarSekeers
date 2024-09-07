using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public int totalCoins = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void AddCoinToPool()
    {
        totalCoins++;
        Debug.Log("Total Coins in Pool: " + totalCoins);
    }
}
