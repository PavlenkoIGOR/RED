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
    public float delay;

    [Header("PrefabsProjectiles")]
    [SerializeField] private Projectile standartPrefab;
    [SerializeField] private Projectile rocketPrefab;

    public bool canShootOnAwake = false;

    private List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();


   [SerializeField] private AudioSource _shotSound;


    private void FixedUpdate()
    {
        if (canShootOnAwake)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {

        if (type == TypeProjectiles.Standart && standartPrefab)
        {
            var stdPref = Instantiate(standartPrefab, transform.position, transform.rotation);

                _shotSound?.Play();
            
            
            canShootOnAwake = false;
            yield return new WaitForSeconds(delay);
            canShootOnAwake = true;
        }
        else if (type == TypeProjectiles.Rocket && rocketPrefab)
        {
            var rocketPref = Instantiate(rocketPrefab, transform.position, transform.rotation);
            canShootOnAwake = false;
            yield return new WaitForSeconds(delay);
            canShootOnAwake = true;
        }
    }

    
   



    
}
