using System;
using UnityEngine;
using UnityEngine.Events;

public class DifficultController : MonoBehaviour
{
    [SerializeField] private BaffSpawner _baffSpawner;
    /*[HideInInspector]*/ public static byte level = 1;
    private byte tmpLvl = 1;

    [SerializeField] private Enemy _blueEnemy;
    [SerializeField] private Enemy _greenEnemy;
    [SerializeField] private Enemy _purpleEnemy;
    [SerializeField] private Enemy _boss;
    [SerializeField] private Hero _hero;

    [SerializeField] private float _percentOfDifficultEnemy = 0.5f;
    [SerializeField] private float _percentOfDifficultHero = 0.1f;

    private float _heroDelay;
    private float _blueEnemyDelay;
    private float _greenEnemyDelay;
    private float _purpleEnemyDelay;
    private float _bossEnemyDelay;

    //public event Action OnLevelChange;
    public static UnityEvent OnLevelChange = new UnityEvent();

    private void Awake()
    {
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
    }
    private void Start()
    {
        OnLevelChange.AddListener(ChangeDifficult);
    }

    private void ChangeDifficult()
    {
        if (tmpLvl != level)
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

            
        }

        //print("level changed");

            tmpLvl = level;
    }
    private void OnDestroy()
    {
        OnLevelChange.RemoveAllListeners();

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
    }
}
