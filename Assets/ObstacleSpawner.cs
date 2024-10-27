using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    public Vector2 spawnAreaMin; // Posición mínima en el área de spawn (x, y)
    public Vector2 spawnAreaMax; // Posición máxima en el área de spawn (x, y)

    [Header("Obstacle Settings")]
    public GameObject obstaclePrefab; // Prefab del obstáculo a instanciar
    public float initialSpawnInterval = 2f; // Intervalo de spawn inicial
    public float spawnIntervalDecrement = 0.05f; // Cantidad de decremento en cada spawn
    public float minimumSpawnInterval = 0.5f; // Intervalo mínimo de spawn

    private float currentSpawnInterval; // Intervalo actual
    private float spawnTimer; // Temporizador para el próximo spawn

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        spawnTimer = currentSpawnInterval;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnObstacle();
            spawnTimer = currentSpawnInterval;

            // Decrementar el tiempo de spawn, sin pasar el mínimo
            currentSpawnInterval = Mathf.Max(currentSpawnInterval - spawnIntervalDecrement, minimumSpawnInterval);
        }
    }

    void SpawnObstacle()
    {
        // Generar una posición aleatoria dentro del área definida
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // Instanciar el obstáculo
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}

