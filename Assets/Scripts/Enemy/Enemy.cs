using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Destructible
{
    [SerializeField] private float delay;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] guns;
    [SerializeField] private Projectile enmProjPrefab;

    [SerializeField] private List<Vector3> massiveWards = new List<Vector3>();

    [SerializeField] private Animator[] _animatorsSmoke;

    private bool canShoot = true;

    private void Awake()
    {
        EnemySpawner.enemyesAlive.Add(this);        
    }

    public void SmokeAnim()
    {
        if (_animatorsSmoke != null && _animatorsSmoke[1].transform.gameObject.activeSelf)
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

    private void Update()
    {
        SmokeAnim();
    }

    protected override void OnDestroy()
    {        
        if (healthBarMain.size.y <= 0)
        {
            for (int i = 0; i < _animatorsSmoke.Length; i++)
            {
                _animatorsSmoke[i].StopPlayback();
            }
        }
        EnemySpawner.enemyesAlive.Remove(this);
        //print($"afterDestroy {EnemySpawner.enemyesAlive.Count}");
        base.OnDestroy();
    }

    public void ActivateShoot()
    {
        StartCoroutine(Shoot());
        StartCoroutine(Move());        
    }



    IEnumerator Shoot()
    {
        while (true)
        {
            for (int i = 0; i < guns.Length; i++)
            {
                Instantiate(enmProjPrefab, guns[i].position, guns[i].rotation);
                canShoot = false;
                yield return new WaitForSeconds(delay);
                canShoot = true;
            }
        }

    }

    IEnumerator Move()
    {
        int i = 0;
        while (currentHitPoints > 0)
        {
            if (i == massiveWards.Count)
            {
                i = 0;
            }
            transform.position = Vector2.MoveTowards(transform.position, massiveWards[i], moveSpeed * Time.deltaTime);
            if (transform.position == massiveWards[i])
            {
                i++;
            }
            yield return null;
        }

    }

}
