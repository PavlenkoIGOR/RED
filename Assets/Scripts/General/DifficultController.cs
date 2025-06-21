using UnityEngine;

public class DifficultController : MonoBehaviour
{
    public byte level = 1;

    public float heroShootDelay;
    public float enemyShootDelay;
    public float bossShootDelay;
    public float enemySpeed;
    public float bossSpeed;
    public float heroSpeed;
    public float enemyHP;
    public float bossHP;
    public float heroHP;

    [SerializeField] private Enemy _blueEnemy;
    [SerializeField] private Enemy _greenEnemy;
    [SerializeField] private Enemy _purpleEnemy;
    [SerializeField] private Enemy _boss;
    [SerializeField] private Hero _hero;

    private void Awake()
    {

    }
    private void Start()
    {
        foreach (Gun gun in _blueEnemy.Guns)
        {
            gun.delay = 0.12f;
        }
    }
}
