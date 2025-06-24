using JetBrains.Annotations;
using SpaceShooter;
using UnityEngine;

public class Hero : Destructible
{
    [SerializeField]private GameObject _shieldedHeroView;
    [SerializeField]private GameObject _mainHeroView;
    public Gun[] guns;
    public GameObject shieldedHeroView { get => _shieldedHeroView; set => _shieldedHeroView = value; }
    public GameObject mainHeroView { get => _mainHeroView; set => _mainHeroView = value; }


    [SerializeField] private float _shieldDuration;
    public float shieldDuration { get => _shieldDuration; set => _shieldDuration = value; }
    [HideInInspector]
    public bool hasShield = false;
    public bool hasRocket = false;


    public GameController controller;

    protected override void Start()
    {
        base.Start();
        _shieldedHeroView.SetActive(false);

    }
    protected override void OnDeath()
    {
        hasShield = false;
        hasRocket = false;
        controller.OnHeroDeath.Invoke();
        base.OnDeath();        
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        var baff = collision.GetComponent<BaffEntity>();
        if (baff != null)
        {
            if (baff is ShieldBaff)
            {
                hasShield = true;
            }
            if (baff is RocketBaff)
            {
                hasRocket = true;
            }
            Destroy(collision.gameObject);
        }
    }
}
