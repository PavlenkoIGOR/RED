using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField] private Enemy _bossPrefab;
    [SerializeField] private List<Enemy> enemiesPrefabs = new List<Enemy>();

    [HideInInspector] public static List<Enemy> enemyesAlive = new List<Enemy>();

    public List<SpawnPoint> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }

    public void SpawnRandomEnemyes(int level)
    {

        //List<Enemy> shuffledPrefabs = new List<Enemy>(enemiesPrefabs);

        //for (int i = 0; i < shuffledPrefabs.Count; i++)
        //{
        //    int randIndex = Random.Range(i, shuffledPrefabs.Count);
        //    Enemy temp = shuffledPrefabs[i];
        //    shuffledPrefabs[i] = shuffledPrefabs[randIndex];
        //    shuffledPrefabs[randIndex] = temp;
        //}

        //int countToSpawn = Mathf.Min(spawnPoints.Count, shuffledPrefabs.Count);

        //for (int i = 0; i < shuffledPrefabs.Count; i++)
        //{
        //    if (shuffledPrefabs[i].canSpawn == true)
        //    { 
        //        var point = spawnPoints[i];
        //        var enmPrefab = Instantiate(shuffledPrefabs[i], point.transform.position, Quaternion.identity);
        //        point.Setposition(enmPrefab);
        //        enemyesAlive.Add(enmPrefab);
        //    }
        //}

        // Очистка текущего списка врагов, если нужно
        // enemyesAlive.Clear();

        

        List<Enemy> enemiesToSpawn = new List<Enemy>();

        if (level == 1)
        {
            // Спавним 1 _enemyBlue
            Enemy blueEnemy = enemiesPrefabs.Find(e => e.name.Contains("EnemyBlue"));
            if (blueEnemy != null) enemiesToSpawn.Add(blueEnemy);
        }
        else if (level == 2)
        {
            // Спавним 2 _enemyBlue
            Enemy blueEnemy = enemiesPrefabs.Find(e => e.name.Contains("EnemyBlue"));
            if (blueEnemy != null)
            {
                enemiesToSpawn.Add(blueEnemy);
                enemiesToSpawn.Add(blueEnemy);
            }
        }
        else if (level == 3)
        {
            // Спавним 3 _enemyBlue
            Enemy blueEnemy = enemiesPrefabs.Find(e => e.name.Contains("EnemyBlue"));
            if (blueEnemy != null)
            {
                for (int i = 0; i < 3; i++)
                    enemiesToSpawn.Add(blueEnemy);
            }
        }
        else if (level >= 4 && level <= 6)
        {
            // Спавним 1-3 _enemyGreen в зависимости от уровня
            Enemy greenEnemy = enemiesPrefabs.Find(e => e.name.Contains("EnemyGreen"));
            int count = level - 3; // уровень 4 -> 1 враг, 5 -> 2 врага, 6 -> 3 врага
            if (greenEnemy != null)
            {
                for (int i = 0; i < count; i++)
                    enemiesToSpawn.Add(greenEnemy);
            }
        }
        else if (level >= 7 && level <= 9)
        {
            // Спавним 1-3 _enemyPurple
            Enemy purpleEnemy = enemiesPrefabs.Find(e => e.name.Contains("EnemyPurple"));
            int count = level - 6; // уровень 7 ->1 враг, 8->2,9->3
            if (purpleEnemy != null)
            {
                for (int i = 0; i < count; i++)
                    enemiesToSpawn.Add(purpleEnemy);
            }
        }
        else
        {
            // Для остальных уровней — как раньше: случайные три врага
            List<Enemy> shuffledPrefabs = new List<Enemy>(enemiesPrefabs);

            // Перемешиваем список
            for (int i = 0; i < shuffledPrefabs.Count; i++)
            {
                int randIndex = Random.Range(i, shuffledPrefabs.Count);
                Enemy temp = shuffledPrefabs[i];
                shuffledPrefabs[i] = shuffledPrefabs[randIndex];
                shuffledPrefabs[randIndex] = temp;
            }

            int countToSpawn = Mathf.Min(spawnPoints.Count, 3); // максимум три врага

            for (int i = 0; i < countToSpawn; i++)
            {
                
                    enemiesToSpawn.Add(shuffledPrefabs[i]);
            }
        }

        // Теперь спавним выбранных врагов по spawnPoints
        int spawnCount = Mathf.Min(spawnPoints.Count, enemiesToSpawn.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            var point = spawnPoints[i];
            var enemyPrefab = Instantiate(enemiesToSpawn[i], point.transform.position, Quaternion.identity);
            point.Setposition(enemyPrefab);
            enemyesAlive.Add(enemyPrefab);
        }
    }
    public void SpawnBoss()
    {
        int index = Random.Range(0, spawnPoints.Count);
        var spawnPoint = spawnPoints[index];
        var enmPrefab = Instantiate(_bossPrefab, spawnPoint.transform.position, Quaternion.identity);
        spawnPoint.Setposition(enmPrefab);
        enemyesAlive.Add(enmPrefab);
    }


}
