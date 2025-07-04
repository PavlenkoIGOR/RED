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


    protected override void Start()
    {
        base.Start();
        _shieldedHeroView.SetActive(false);

    }

    private void Update()
    {
        SmokeAnim();
    }
    protected override void OnDeath()
    {
        Player.instance.hasShield = false;
        Player.instance.hasRocket = false;

        GameController.OnHeroDeath.Invoke();
        base.OnDeath();        
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        var baff = collision.GetComponent<BaffEntity>();
        if (baff != null)
        {
            if (baff is ShieldBaff)
            {
                Player.instance.hasShield = true;
            }
            if (baff is RocketBaff)
            {
                Player.instance.hasRocket = true;
            }
            Destroy(collision.gameObject);
        }
        if (collision.transform.root.GetComponent<Enemy>())
        {
            OnDeath();
        }
    }


    [SerializeField] private Animator[] _animatorsSmoke;
    public void SmokeAnim()
    {
        if (_animatorsSmoke != null)
        {
                if (healthBarMain.size.y <= _originalSizeY / 3 * 2 & healthBarMain.size.y > _originalSizeY / 3)
                {
                    _animatorsSmoke[0].Play("SmokeAnim1");
                }
                if (healthBarMain.size.y <= _originalSizeY / 3)
                {
                    _animatorsSmoke[1].Play("SmokeAnim1");
                }
            
        }
    }
}
