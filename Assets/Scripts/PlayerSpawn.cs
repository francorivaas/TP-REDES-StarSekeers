using Photon.Pun;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, 
            new Vector2(Random.Range(-4, 4), 
            Random.Range(-4, 4)), Quaternion.identity);
    }
}
