using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructible
{
    public float moveSpeed;
    //public bool canSpawn { get; set; } = false;
    [SerializeField] private Gun[] guns;
    [SerializeField] private Projectile enmProjPrefab;

    [SerializeField] private List<Vector3> massiveWards = new List<Vector3>();

    [SerializeField] private Animator[] _animatorsSmoke;
    public TypeEntity enemyType;

    private bool canShoot = true;

    public Gun[] Guns { get { return guns; } }




    float screenLeft;
    float screenRight;
    float screenBottom;
    float screenTop;


    private void Awake()
    {
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x;
        screenBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
        screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)).y;


        // �����������: ��������� ���������� x ����� ��������� ����������
        Vector3 temp = massiveWards[0];
        temp.x = screenLeft + 0.3f;
        massiveWards[0] = temp;

        temp = massiveWards[massiveWards.Count-1];
        temp.x = screenRight - 0.3f;
        massiveWards[massiveWards.Count - 1] = temp;

        //EnemySpawner.enemyesAlive.Add(this);
        isDestructible = false;
    }

    public void SmokeAnim()
    {
        if (_animatorsSmoke != null && _animatorsSmoke[1].transform.gameObject.activeSelf)
        {
            if (enemyType == TypeEntity.Enemy)
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

            else if (enemyType == TypeEntity.Boss)
            {
                if (healthBarMain.size.y <= _originalSizeY / 4 * 3 & healthBarMain.size.y > _originalSizeY / 2)
                {
                    _animatorsSmoke[0].Play("SmokeAnim1");
                }
                if (healthBarMain.size.y <= _originalSizeY / 2 & healthBarMain.size.y > _originalSizeY / 4)
                {
                    _animatorsSmoke[1].Play("SmokeAnim1");
                }
                if (healthBarMain.size.y <= _originalSizeY / 4)
                {
                    _animatorsSmoke[2].Play("SmokeAnim1");
                }
            }
        }
    }

    private void Update()
    {
        SmokeAnim();
    }

    protected override void OnDeath()
    {
        moveSpeed = 0;
        base.OnDeath();
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
        base.OnDestroy();
    }

    public void ActivateMove()
    {
        StartCoroutine(Move());        
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
