using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] private Enemy _bossPrefab;
    [SerializeField] private List<Enemy> enemiesPrefabs = new List<Enemy>();

    [HideInInspector] public static List<Enemy> enemyesAlive = new List<Enemy>();

    public List<SpawnPoint> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }

    public void SpawnRandomEnemyes()
    {
        // Создаем копию списка, чтобы не менять оригинал
        List<Enemy> shuffledPrefabs = new List<Enemy>(enemiesPrefabs);
        // Перемешиваем список
        for (int i = 0; i < shuffledPrefabs.Count; i++)
        {
            int randIndex = Random.Range(i, shuffledPrefabs.Count);
            Enemy temp = shuffledPrefabs[i];
            shuffledPrefabs[i] = shuffledPrefabs[randIndex];
            shuffledPrefabs[randIndex] = temp;
        }

        int countToSpawn = Mathf.Min(spawnPoints.Count, shuffledPrefabs.Count);

        for (int i = 0; i < countToSpawn; i++)
        {
            var point = spawnPoints[i];
            var prefab = shuffledPrefabs[i];
            var enmPrefab = Instantiate(prefab, point.transform.position, Quaternion.identity);
            point.Setposition(enmPrefab);
            enemyesAlive.Add(enmPrefab);
        }
    }
    public void SpawnBoss()
    {
        int index = Random.Range(0, spawnPoints.Count);
        var spawnPoint = spawnPoints[index];
        print($"{index}");
        var enmPrefab = Instantiate(_bossPrefab, spawnPoint.transform.position, Quaternion.identity);
        spawnPoint.Setposition(enmPrefab);
        enemyesAlive.Add(enmPrefab);
    }
}
