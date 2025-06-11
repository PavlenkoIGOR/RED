using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый объект на сцене. То что может иметь хит поинты.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] private bool _isDestructible;
        public bool isDestructible => _isDestructible;

        /// <summary>
        /// Стартовое кол-во хитпоинтов.
        /// </summary>
        [SerializeField] private int _hitPoints;
        
        /// <summary>
        /// Текущие хит поинты
        /// </summary>
        private int _currentHitPoints;
        protected int currentHitPoints => _currentHitPoints;

        [SerializeField] private SpriteRenderer _healthBarMain;
        protected SpriteRenderer healthBarMain => _healthBarMain;

        protected float _originalSizeY;

        #endregion

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
                OnDeath();
        }

        public void AddHitPoints(float hp)
        {
            _currentHitPoints = (int)Mathf.Clamp(_currentHitPoints + hp, 0, _hitPoints);
        }

        #endregion

        /// <summary>
        /// Перепоределяемое событие уничтожения объекта, когда хит поинты ниже нуля.
        /// </summary>
        protected virtual void OnDeath()
        {
            //if (m_ExplosionPrefab != null)
            //{
            //    var explosion = Instantiate(m_ExplosionPrefab.gameObject);
            //    explosion.transform.position = transform.position;
            //}
            Player.instance.AddScore(scoreValue);

            Destroy(gameObject);

            m_EventOnDeath?.Invoke();
        }

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        //[SerializeField] private ImpactEffect m_ExplosionPrefab;

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
    }
}