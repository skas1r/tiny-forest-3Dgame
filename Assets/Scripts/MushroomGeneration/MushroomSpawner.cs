using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    public static MushroomSpawner Instance { get; private set; }

    [Header("Mushrooms to spawn")]
    [SerializeField] private GameObject badMushroomPrefab;
    [SerializeField] private GameObject goodMushroomPrefab;

    [Header("Spawning parameters")]
    [SerializeField] private int initialCount = 30;
    [SerializeField] private float minimalLevelY = 70f;
    [SerializeField] private float minX = -500f, maxX = 500f;
    [SerializeField] private float minZ = -1000f, maxZ = 1000f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Початкова генерація
        for (int i = 0; i < initialCount; i++)
        {
            SpawnMushroom(true);  // Поганий гриб
            SpawnMushroom(false); // Хороший гриб
        }
    }

    public void SpawnMushroom(bool isBadMushroom)
    {
        Vector3 spawnPos = Vector3.zero;
        bool validPositionFound = false;
        int attempts = 0;

        // Обмеження у 100 спроб
        while (!validPositionFound && attempts < 100)
        {
            // Точка старту променя
            Vector3 rayStart = new Vector3(Random.Range(minX, maxX), 1000f, Random.Range(minZ, maxZ));
            // Випускаємо промінь вниз
            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 2000f))
            {
                // Перевіряємо, чи влучили саме в землю, і чи висота більша за мінімальний рівень
                if (hit.collider.GetComponent<Terrain>() != null && hit.point.y > minimalLevelY)
                {
                    spawnPos = hit.point;
                    validPositionFound = true;
                }
            }
            attempts++;
        }

        if (validPositionFound)
        {
            GameObject prefabToSpawn = isBadMushroom ? badMushroomPrefab : goodMushroomPrefab;
            // Генерація гриба
            Instantiate(prefabToSpawn, spawnPos, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
        }
    }
}