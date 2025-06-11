using SpaceShooter;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : Destructible
{
    [SerializeField] private Animator _animExplosion;
    private void Update()
    {
        ShipExlosionAnim();
    }
    private void ShipExlosionAnim()
    {
        if (currentHitPoints <= 0)
        {
            _animExplosion.Play("HeroExplosionAnimation");
        }
    }

    protected override void OnDestroy()
    {
        _animExplosion.StopPlayback();
    }

}
