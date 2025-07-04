using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] private bool _isDestructible;
        public bool isDestructible { get { return _isDestructible; } set { _isDestructible = value; } }

        /// <summary>
        /// Стартовое кол-во хитпоинтов.
        /// </summary>
        public int _hitPoints;
        private int _currentHitPoints;
        public int currentHitPoints => _currentHitPoints;

        [SerializeField] private SpriteRenderer _healthBarMain;
        protected SpriteRenderer healthBarMain => _healthBarMain;

        protected float _originalSizeY;

        [SerializeField] private Animator _animShipExplosion;
        [SerializeField] private GameObject _viewExplosion;
        #endregion

        [SerializeField] private AudioSource _shipExplosionSound;
        #region Unity events





        protected virtual void Start()
        {
            _currentHitPoints = _hitPoints;
            _originalSizeY = _healthBarMain.size.y;



        }

        #region Безтеговая коллекция скриптов на сцене

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);

        }

        #endregion 

        #endregion

        #region Public API

        /// <summary>
        /// Применение дамага к объекту.
        /// </summary>
        /// <param name="damage"></param>
        public void ApplyDamage(int damage)
        {
            if (!_isDestructible)
                return;

            _currentHitPoints -= damage;
            if (_healthBarMain)
            {
                float healthRatio = (float)_currentHitPoints * _originalSizeY / _hitPoints;
                _healthBarMain.size = new Vector2(_healthBarMain.size.x, healthRatio);
            }


            if (_currentHitPoints <= 0)
            {
                OnDeath();
            }
                
        }

        public void AddHitPoints(float hp)
        {
            _currentHitPoints = (int)Mathf.Clamp(_currentHitPoints + hp, 0, _hitPoints);
        }

        #endregion

        protected virtual void OnDeath()
        {
            //print("onDeath");
            if (transform.tag == "Boss")
            {
                DifficultController.level++;
                DifficultController.OnLevelChange.Invoke();
            }

            if (transform.tag == "Player")
            {
                ShipController sContr = transform.GetComponent<ShipController>();
                sContr.enabled = false;
            }
            if (transform.name.Contains("Enemy"))
            {
                Enemy enemyComp = transform.GetComponent<Enemy>();
            }
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(false);
            }

            ViewExplosionSetActive();
            ShipExplosionSoundPlay();


            StartCoroutine(PLayExplosion());

            Player.instance.AddScore(scoreValue);

            m_EventOnDeath?.Invoke();
        }
        void ViewExplosionSetActive()
        {
            _viewExplosion.SetActive(true);
        }
        void ShipExplosionSoundPlay()
        {
            _shipExplosionSound.Play();
        }






        public void StartDeathEnemy()
        {
            OnDeath();
        }
        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        #region Teams

        /// <summary>
        /// Полностью нейтральный тим ид. Боты будут игнорировать такие объекты.
        /// </summary>
        public const int TeamIdNeutral = 0;

        /// <summary>
        /// ИД стороны. Боты будут атаковать всех кто не свой.
        /// </summary>
        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        #endregion

        #region Score

        /// <summary>
        /// Кол-во очков за уничтожение.
        /// </summary>
        [SerializeField] private int _scoreValue;
        public int scoreValue => _scoreValue;

        #endregion





        #region animation
        private IEnumerator PLayExplosion()
        {
            if (_animShipExplosion != null)
            {
                if (transform.tag == "Player")
                {
                    //print("heroExplosionAnim");
                    _animShipExplosion.Play("HeroExplosionAnimation");

                    float clipLength = default;
                    RuntimeAnimatorController rac = _animShipExplosion.runtimeAnimatorController;
                    for (int i = 0; i < rac.animationClips.Length; i++)
                    {
                        if (rac.animationClips[i].name == "HeroExplosionAnimation")
                        {
                            clipLength = rac.animationClips[i].length;
                        }
                    }

                    yield return new WaitForSeconds(clipLength);

                }

                if (transform.name.Contains("Blue"))
                {
                    _animShipExplosion.Play("BlueShipExplosion");

                    float clipLength = default;
                    RuntimeAnimatorController rac = _animShipExplosion.runtimeAnimatorController;
                    for (int i = 0; i < rac.animationClips.Length; i++)
                    {
                        if (rac.animationClips[i].name == "BlueShipExplosion")
                        {
                            clipLength = rac.animationClips[i].length;
                        }
                    }

                    yield return new WaitForSeconds(clipLength);
                }
                if (transform.name.Contains("Green"))
                {
                    _animShipExplosion.Play("GreenExplosionAnimation");

                    float clipLength = default;
                    RuntimeAnimatorController rac = _animShipExplosion.runtimeAnimatorController;
                    for (int i = 0; i < rac.animationClips.Length; i++)
                    {
                        if (rac.animationClips[i].name == "GreenExplosionAnimation")
                        {
                            clipLength = rac.animationClips[i].length;
                        }
                    }

                    yield return new WaitForSeconds(clipLength);
                }
                if (transform.name.Contains("Purple"))
                {
                    _animShipExplosion.Play("VioletShipExplosionaAnimation");

                    float clipLength = default;
                    RuntimeAnimatorController rac = _animShipExplosion.runtimeAnimatorController;
                    for (int i = 0; i < rac.animationClips.Length; i++)
                    {
                        if (rac.animationClips[i].name == "VioletShipExplosionaAnimation")
                        {
                            clipLength = rac.animationClips[i].length;
                        }
                    }

                    yield return new WaitForSeconds(clipLength);
                }
                if (transform.name.Contains("Boss"))
                {
                    _animShipExplosion.Play("BossExplosionAnim");

                    float clipLength = default;
                    RuntimeAnimatorController rac = _animShipExplosion.runtimeAnimatorController;
                    for (int i = 0; i < rac.animationClips.Length; i++)
                    {
                        if (rac.animationClips[i].name == "BossExplosionAnim")
                        {
                            clipLength = rac.animationClips[i].length;
                        }
                    }

                    yield return new WaitForSeconds(clipLength);
                }

                Destroy(gameObject);
            }

        }
        #endregion
    }
}
