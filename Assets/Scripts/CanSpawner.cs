using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSpawner : MonoBehaviour
{
    [SerializeField] private GameObject canPrefab;

    public float timer;
    public float currentTime;

    private void Start()
    {
        currentTime = 0;    
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timer)
        {
            Instantiate(canPrefab, new Vector2(Random.Range(-8, 8), Random.Range(-8, 8)), Quaternion.identity);
            currentTime = 0;
        }
    }
}
