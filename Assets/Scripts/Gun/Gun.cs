using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TypeProjectiles
{
    Standart,
    Rocket
}

public class Gun : MonoBehaviour
{

    [Header("Properties")]
    [SerializeField] private TypeProjectiles type;
    [SerializeField] private float delay;

    [Header("PrefabsProjectiles")]
    [SerializeField] private Projectile standartPrefab;
    [SerializeField] private Projectile rocketPrefab;

    private bool canShoot = true;

    private List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();


    [SerializeField] private AudioSource _shotSound;

    private void Update()
    {
        if (canShoot)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {

        if (type == TypeProjectiles.Standart && standartPrefab)
        {
            var stdPref = Instantiate(standartPrefab, transform.position, Quaternion.identity);
            _shotSound?.Play();
            canShoot = false;
            yield return new WaitForSeconds(delay);
            canShoot = true;
        }
        else if (type == TypeProjectiles.Rocket && rocketPrefab)
        {
            var rocketPref = Instantiate(rocketPrefab, transform.position, Quaternion.identity);
            canShoot = false;
            yield return new WaitForSeconds(delay);
            canShoot = true;
        }
    }

    
   



    
}
