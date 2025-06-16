using SpaceShooter;
using System.Collections;
using UnityEngine;

public class Hero : Destructible
{
    [SerializeField]private GameObject _shieldedHeroView;
    [SerializeField]private GameObject _mainHeroView;
    public GameObject shieldedHeroView { get => _shieldedHeroView; set => _shieldedHeroView = value; }
    public GameObject mainHeroView { get => _mainHeroView; set => _mainHeroView = value; }


    [SerializeField] private float _shieldDuration;
    public float shieldDuration { get => _shieldDuration; set => _shieldDuration = value; }
    [HideInInspector]
    public bool hasShield = false;


    public GameController controller;

    protected override void Start()
    {
        base.Start();
        _shieldedHeroView.SetActive(false);

    }
    protected override void OnDeath()
    {
        hasShield = false;
        controller.OnHeroDeath.Invoke();
        base.OnDeath();        
    }
    private void Update()
    {
        //print($"{currentHitPoints}");
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        var shieldBaff = collision.GetComponent<ShieldBaff>();
        if (shieldBaff != null)
        {
            hasShield = true;
            Destroy(collision.gameObject);
        }
    }
}
