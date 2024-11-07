using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private float timeInterval; // Tiempo en segundos entre cada chequeo
    private float timer;
    private bool isTrue;
    [SerializeField] private float chances;
    private PhotonView pv;

    [SerializeField] private List<GameObject> PowerUpsList;
    
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeInterval)
        {
            timer = 0;
            isTrue = Random.value > chances;

            if (isTrue)
            {
                int randomValue = Random.Range(0, PowerUpsList.Count);
            PhotonNetwork.Instantiate(PowerUpsList[randomValue].name, 
                    new Vector2(Random.Range(-4, 4), 
                        Random.Range(-8, 8)), Quaternion.identity);
            }
        }
    }
}