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
        foreach (var point in spawnPoints)
        {
            var x = Random.Range(0, enemiesPrefabs.Count);
            var enmPrefab = Instantiate(enemiesPrefabs[x], point.transform.position, Quaternion.identity);
            point.Setposition(enmPrefab);

        }
    }
    public void SpawnBoss()
    {
        var x = Random.Range(0, enemiesPrefabs.Count);
        var enmPrefab = Instantiate(_bossPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.identity);
        spawnPoints[Random.Range(0, spawnPoints.Count)].Setposition(enmPrefab);

    }
}
