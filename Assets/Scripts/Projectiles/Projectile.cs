using SpaceShooter;
using UnityEngine;


public enum TypeEntity
{
    Hero,
    Enemy,
    Boss
}
public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float velocityEnm;

    [SerializeField] private float lifeTime;
    [SerializeField] private float lifeTimeEnm;
    [SerializeField] private float lifeTimeBoss;

    [SerializeField] private int damage;


    [SerializeField] private TypeEntity type;

    private void Start()
    {
        if (type == TypeEntity.Hero)
        {
            Destroy(gameObject, lifeTime);
        }
        else if (type == TypeEntity.Enemy)
        {
            Destroy(gameObject, lifeTimeEnm);
        }
        else if (type == TypeEntity.Boss)
        {
            Destroy(gameObject, lifeTimeBoss);
        }
    }
    private void Update()
    {
        if (type == TypeEntity.Hero)
        {
            float stepLength = Time.deltaTime * velocity;

            Vector2 step = transform.up * stepLength;


            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                Enemy enm = hit.collider.transform.root.GetComponent<Enemy>();
                if (enm != null)
                {
                    enm.ApplyDamage(damage);
                    OnProjectileLifeEnd(hit.collider, hit.point);
                }

            }
            transform.position += new Vector3(step.x, step.y, 0);
        }

        else if (type == TypeEntity.Enemy)
        {
            float stepLength = Time.deltaTime * velocityEnm;

            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                Destructible hero = hit.collider.transform.root.GetComponent<Destructible>();
                if (hero != null)
                {
                    hero.ApplyDamage(damage);

                    OnProjectileLifeEnd(hit.collider, hit.point);
                }
            }
            transform.position += new Vector3(step.x, step.y, 0);
        }
    }

    private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
    {

        Destroy(gameObject);
    }
}


