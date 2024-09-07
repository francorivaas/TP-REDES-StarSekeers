using Photon.Pun;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeToStartSpawning;
    [SerializeField] private float timeBetweenSpawn;

    private bool readyToSpawn;
    private float timer;
    private bool matchStarted = false;
    private void Start()
    {
        timer = 0;
    }
    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            matchStarted = true;
            if (PhotonNetwork.IsMasterClient)
            {
                timer += Time.deltaTime;
                if (!readyToSpawn && timeToStartSpawning < timer)
                {
                    readyToSpawn = true;
                    timer = 0;
                }
            }
            if (readyToSpawn && timer > timeBetweenSpawn)
            {
                timer = 0;
                PhotonNetwork.Instantiate(enemyPrefab.name, new Vector2(Random.Range(-4, 4), Random.Range(-4, 4)), Quaternion.identity);
            }
        }
    }
}
