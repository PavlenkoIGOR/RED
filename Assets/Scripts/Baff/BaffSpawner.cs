using UnityEngine;

public class BaffSpawner : MonoBehaviour
{
    float screenLeft;
    float screenRight;
    float screenBottom;
    float screenTop;

    [Header("Shield")]
    public float spawnTime_shield = 10.0f;
    [SerializeField] private GameObject _droppingShield;
    private float _timeShield_tmp = 0;

    [Header("Rocket")]
    public float spawnTime_Rocket = 10.0f;
    [SerializeField] private GameObject _droppingRocket;
    private float _timeRocket_tmp = 0;

    [SerializeField] private bool _canSpawnShield;
    public bool canSpawnShield { get => _canSpawnShield; set => _canSpawnShield = value; }

    [SerializeField] private bool _canSpawnRocket;
    public bool canSpawnRocket { get => _canSpawnRocket; set => _canSpawnRocket = value; }

    void Start()
    {
        // ѕолучение границ видимой области камеры
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x;
        screenBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
        screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)).y;
    }

    void Update()
    {
        if (canSpawnShield)
        {
            if (_timeShield_tmp >= spawnTime_shield)
            {
                var ds = Instantiate(_droppingShield);
                _droppingShield.transform.position = new Vector2(Random.Range(screenLeft, screenRight), screenTop + 1.0f);
                _timeShield_tmp = 0;
                spawnTime_shield = Random.Range(5, spawnTime_shield);
            }
            else
            {
                _timeShield_tmp += Time.deltaTime;
            }
        }
        if (canSpawnRocket)
        {
            if (_timeRocket_tmp >= spawnTime_Rocket)
            {
                var ds = Instantiate(_droppingRocket);
                _droppingRocket.transform.position = new Vector2(Random.Range(screenLeft, screenRight), screenTop + 1.0f);
                _timeRocket_tmp = 0;
                spawnTime_shield = Random.Range(5, spawnTime_Rocket);
            }
            else
            {
                _timeRocket_tmp += Time.deltaTime;
            }
        }
    }
}
