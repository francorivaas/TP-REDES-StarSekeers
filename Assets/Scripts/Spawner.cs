using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Area")]
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    [Header("Obstacle Settings")]
    public List<GameObject> obstaclePrefabs; // Lista de prefabs de obstáculos
    public float initialObstacleInterval = 2f;
    public float obstacleIntervalDecrement = 0.05f;
    public float minimumObstacleInterval = 0.5f;

    [Header("PowerUp Settings")]
    public List<GameObject> powerUpPrefabs; // Lista de prefabs de PowerUps
    public float initialPowerUpInterval = 5f;
    public float powerUpIntervalDecrement = 0.1f;
    public float minimumPowerUpInterval = 1f;

    private float currentObstacleInterval;
    private float obstacleTimer;

    private float currentPowerUpInterval;
    private float powerUpTimer;

    void Start()
    {
        currentObstacleInterval = initialObstacleInterval;
        obstacleTimer = currentObstacleInterval;

        currentPowerUpInterval = initialPowerUpInterval;
        powerUpTimer = currentPowerUpInterval;
    }

    void Update()
    {
        // Control de spawn de obstáculos
        obstacleTimer -= Time.deltaTime;
        if (obstacleTimer <= 0)
        {
            SpawnObject(obstaclePrefabs);
            obstacleTimer = currentObstacleInterval;
            currentObstacleInterval = Mathf.Max(currentObstacleInterval - obstacleIntervalDecrement, minimumObstacleInterval);
        }

        // Control de spawn de PowerUps
        powerUpTimer -= Time.deltaTime;
        if (powerUpTimer <= 0)
        {
            SpawnObject(powerUpPrefabs);
            powerUpTimer = currentPowerUpInterval;
            currentPowerUpInterval = Mathf.Max(currentPowerUpInterval - powerUpIntervalDecrement, minimumPowerUpInterval);
        }
    }

    void SpawnObject(List<GameObject> prefabs)
    {
        if (prefabs.Count == 0) return;

        // Seleccionar un prefab aleatorio de la lista
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Count)];

        // Generar una posición aleatoria dentro del área definida
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // Instanciar el objeto
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}