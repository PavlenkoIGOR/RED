using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructible
{
    [SerializeField] private float delay;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform gun;
    [SerializeField] private Projectile enmProjPrefab;

    [SerializeField] private List<Vector3> massiveWards = new List<Vector3>();


    private bool canShoot = true;
  
    private void Awake()
    {
        EnemySpawner.enemyesAlive.Add(this);
        
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
            
            Instantiate(enmProjPrefab, gun.position, gun.rotation);
            canShoot = false;
            yield return new WaitForSeconds(delay);
            canShoot = true;
        }
      
    }

    IEnumerator Move()
    {
        int i = 0;
        while (true)
        {
            if(i == massiveWards.Count)
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
