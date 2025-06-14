using UnityEngine;

    public class Player : SingletonBase<Player>
    {

        [SerializeField] private int _livesQuantity;


        private int _score;
        private int _numKills;

        public int score => _score;
        public int numKills => _numKills;
        public int numLives => _livesQuantity;
        // Start is called before the first frame update
        void Start()
        {
            Respawn();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnShipDeath()
        {
            //Debug.Log("OnshipDeathMethod");
            _livesQuantity--;
            //Debug.Log($"_livesQuantity {_livesQuantity}");
            if (_livesQuantity > 0)
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            //var newPlayerShip = Instantiate(shipPrefab);

            //_ship = newPlayerShip.GetComponent<SpaceShip>();

            //_cameraController.SetTarget(_ship.transform);
            //_movementController.SetTargetShip(_ship);
            //_ship.eventOnDeath.AddListener(OnShipDeath);
        }

        public void AddScore(int num)
        {
            _score += num;
        }

        public void AddKill()
        {
            _numKills++;
        }
    }

