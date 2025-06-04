using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float delay;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform gun;
    [SerializeField] private Projectile enmProjPrefab;

    [SerializeField] private List<Vector3> massiveWards = new List<Vector3>();
    public float Health => health;

    private bool canShoot = true;
  
    private void Awake()
    {
        EnemySpawner.enemyesAlive.Add(this);
        
    }
    public void ApplyDamage(float damage)
    { 
        health -= damage;

        if(health <= 0)
        {
            DamageSelf();
        }
    }



    public void ActivateShoot()
    {
       
        StartCoroutine(Shoot());
        StartCoroutine(Move());
    }

    private void DamageSelf()
    {
        EnemySpawner.enemyesAlive.Remove(this);
        Destroy(gameObject);

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
