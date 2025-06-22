using System;
using UnityEngine;
using UnityEngine.Events;

public class DifficultController : MonoBehaviour
{
    /*[HideInInspector]*/ public static byte level = 1;
    private byte tmpLvl = 1;
    /*
    public float heroShootDelay;
    public float enemyShootDelay;
    public float bossShootDelay;
    public float enemySpeed;
    public float bossSpeed;
    public float heroSpeed;
    public float enemyHP;
    public float bossHP;
    public float heroHP;
    */
    [SerializeField] private Enemy _blueEnemy;
    [SerializeField] private Enemy _greenEnemy;
    [SerializeField] private Enemy _purpleEnemy;
    [SerializeField] private Enemy _boss;
    [SerializeField] private Hero _hero;

    [SerializeField] private float _percentOfDifficultEnemy = 0.5f;
    [SerializeField] private float _percentOfDifficultHero = 0.1f;

    //public event Action OnLevelChange;
    public static UnityEvent OnLevelChange = new UnityEvent();

    private void Awake()
    {
       
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

        print("level changed");

            tmpLvl = level;
    }
    private void OnDestroy()
    {
        OnLevelChange.RemoveAllListeners();
    }
}
