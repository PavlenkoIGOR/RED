using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;


    private void Start()
    {
        spawner.SpawnRandomEnemyes();
    }
}
