using SpaceShooter;
using UnityEngine;

public class Hero : Destructible
{
    [SerializeField]private GameObject _shieldView;
    protected override void Start()
    {
        base.Start();
        _shieldView.SetActive(false);
    }

    private void Update()
    {
        
    }
}
