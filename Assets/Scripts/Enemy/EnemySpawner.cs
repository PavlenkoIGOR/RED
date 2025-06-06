using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] private List<Enemy> enemiesPrefabs = new List<Enemy>();

    [HideInInspector] public static List<Enemy> enemyesAlive = new List<Enemy>();
    public void SpawnRandomEnemyes()
    {
        foreach(var point in spawnPoints)
        { 
            var x = Random.Range(0, enemiesPrefabs.Count);
            var enmPrefab =  Instantiate(enemiesPrefabs[x], point.transform.position, Quaternion.identity);
            point.Setposition(enmPrefab);
        }
    }

}
