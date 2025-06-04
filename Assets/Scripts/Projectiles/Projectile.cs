using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public enum TypeProj
{
    Hero,
    Enemy
}
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float velocity;
        [SerializeField] private float velocityEnm;

        [SerializeField] private float lifeTime;
        [SerializeField] private float lifeTimeEnm;

        [SerializeField] private int damage;


        [SerializeField] private TypeProj type;

    private void Awake()
    {
        if(type == TypeProj.Hero) 
        Destroy(gameObject, lifeTime);

        else
        {
            Destroy(gameObject, lifeTimeEnm);
        }
    }
     private void Update()
        {
           

        if (type == TypeProj.Hero)
        {
            float stepLength = Time.deltaTime * velocity;

            Vector2 step = transform.up * stepLength;


            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                Enemy enm = hit.collider.transform.root.GetComponent<Enemy>();
                if (enm != null)              
                    enm.ApplyDamage(damage);

                OnProjectileLifeEnd(hit.collider, hit.point);

            }
            transform.position += new Vector3(step.x, step.y, 0);
        }

        else if (type == TypeProj.Enemy)
        {
            float stepLength = Time.deltaTime * velocityEnm;

            Vector2 step = transform.up * stepLength;


            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                ShipController hero = hit.collider.transform.root.GetComponent<ShipController>();
                //if (enm != null)
                  //  enm.ApplyDamage(damage);

                OnProjectileLifeEnd(hit.collider, hit.point);

            }
            transform.position += new Vector3(step.x, step.y, 0);
        }


       
        }

        
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            
            Destroy(gameObject);
           
           
        }
       
    
      
    }


