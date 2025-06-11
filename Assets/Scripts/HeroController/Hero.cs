using SpaceShooter;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : Destructible
{
    [SerializeField] private Animator _animExplosion;

    [SerializeField]  private GameObject _heroExplosionView;

    protected override void OnDeath()
    {
        Player.instance.AddScore(scoreValue);

        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }
        _heroExplosionView.SetActive(true);
        StartCoroutine(PLayHeroExplosion());
    }

    protected override void OnDestroy()
    {        
        _animExplosion.StopPlayback();
    }

    private IEnumerator PLayHeroExplosion()
    {
        {
            _animExplosion.Play("HeroExplosionAnimation");

            float clipLength = default;
            RuntimeAnimatorController rac = _animExplosion.runtimeAnimatorController;
            for (int i = 0; i < rac.animationClips.Length; i++)
            {
                if (rac.animationClips[i].name == "HeroExplosionAnimation")
                {
                    clipLength = rac.animationClips[i].length;
                }
            }

            yield return new WaitForSeconds(clipLength);

            Destroy(gameObject);
        }
    }
}
