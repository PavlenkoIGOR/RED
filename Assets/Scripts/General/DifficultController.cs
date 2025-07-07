using System;
using UnityEngine;
using UnityEngine.Events;

public class DifficultController : MonoBehaviour
{
    [SerializeField] private BaffSpawner _baffSpawner;

    public byte level = 0; //change in Distructible
    private byte tmpLvl = 1;

    [SerializeField] private Enemy _blueEnemy;
    [SerializeField] private Enemy _greenEnemy;
    [SerializeField] private Enemy _purpleEnemy;
    [SerializeField] private Enemy _boss;
    [SerializeField] private Hero _hero;

    [SerializeField] private float _percentOfDifficultEnemy = 0.02f;
    [SerializeField] private float _percentOfDifficultHero = 0.04f;

    private float _heroDelay;
    private float _blueEnemyDelay;
    private float _greenEnemyDelay;
    private float _purpleEnemyDelay;
    private float _bossEnemyDelay;

    private int _heroHealth;
    private int _blueEnemyHealth;
    private int _greenEnemyHealth;
    private int _purpleEnemyHealth;
    private int _bossHealth;

    #region timer
    public float _timer_tmp { get; set; } = 0;
    public float _spawnTime = 10.0f;
    #endregion
    //public event Action OnLevelChange;
    public static UnityEvent OnBossDestroyChangeDifficult = new UnityEvent();

    private void Awake()
    {
        //_blueEnemy.canSpawn = true;
        level = 0;
        foreach (Gun gun in _blueEnemy.Guns)
        {
            _blueEnemyDelay = gun.delay;
        }
        foreach (Gun gun in _greenEnemy.Guns)
        {
            _greenEnemyDelay = gun.delay;
        }
        foreach (Gun gun in _purpleEnemy.Guns)
        {
            _purpleEnemyDelay = gun.delay;
        }
        foreach (Gun gun in _boss.Guns)
        {
            _bossEnemyDelay = gun.delay;
        }
        foreach (Gun gun in _hero.guns)
        {
            _heroDelay = gun.delay;
        }

        _heroHealth = _hero._hitPoints;
        _blueEnemyHealth = _blueEnemy._hitPoints;
        _greenEnemyHealth = _greenEnemy._hitPoints;
        _purpleEnemyHealth = _purpleEnemy._hitPoints;
        _bossHealth = _boss._hitPoints;
    }
    private void Start()
    {
        OnBossDestroyChangeDifficult.AddListener(ChangeDifficult);
    }

    private void ChangeDifficult()
    {
        foreach (Gun gun in _blueEnemy.Guns)
        {
            gun.delay = gun.delay - gun.delay * _percentOfDifficultEnemy;
        }
        foreach (Gun gun in _greenEnemy.Guns)
        {
            gun.delay = gun.delay - gun.delay * _percentOfDifficultEnemy;
        }
        foreach (Gun gun in _purpleEnemy.Guns)
        {
            gun.delay = gun.delay - gun.delay * _percentOfDifficultEnemy;
        }
        foreach (Gun gun in _boss.Guns)
        {
            gun.delay = gun.delay - gun.delay * _percentOfDifficultEnemy;
        }
        foreach (Gun gun in _hero.guns)
        {
            gun.delay = gun.delay - gun.delay * _percentOfDifficultHero;
        }

        _boss._hitPoints = _boss._hitPoints * (int)_percentOfDifficultEnemy + _boss._hitPoints;
        _blueEnemy._hitPoints = _blueEnemy._hitPoints * (int)_percentOfDifficultEnemy + _blueEnemy._hitPoints;
        _greenEnemy._hitPoints = _greenEnemy._hitPoints * (int)_percentOfDifficultEnemy + _greenEnemy._hitPoints;
        _purpleEnemy._hitPoints = _purpleEnemy._hitPoints * (int)_percentOfDifficultEnemy + _purpleEnemy._hitPoints;


        _spawnTime -= 0.05f;

        tmpLvl = level;
    }
    private void OnDestroy()
    {
        OnBossDestroyChangeDifficult.RemoveAllListeners();

        ResetParams();
    }

    public void ResetParams()
    {
        foreach (Gun gun in _blueEnemy.Guns)
        {
            gun.delay = _blueEnemyDelay;
        }
        foreach (Gun gun in _greenEnemy.Guns)
        {
            gun.delay = _greenEnemyDelay;
        }
        foreach (Gun gun in _purpleEnemy.Guns)
        {
            gun.delay = _purpleEnemyDelay;
        }
        foreach (Gun gun in _boss.Guns)
        {
            gun.delay = _bossEnemyDelay;
        }
        foreach (Gun gun in _hero.guns)
        {
            gun.delay = _heroDelay;
        }

        _boss._hitPoints = _bossHealth;
        _blueEnemy._hitPoints = _blueEnemyHealth;
        _greenEnemy._hitPoints = _greenEnemyHealth;
        _purpleEnemy._hitPoints = _purpleEnemyHealth;

        level = 0;
    }

}
