using UnityEngine;

public class HealthBarMove : MonoBehaviour
{
    Vector3 LeftScreenBorder;
    [SerializeField] private GameObject _healthBarBGNode;
    SpriteRenderer _healthBarSpriteRenderer;


    float _objectLeftPos;  // Левая граница объекта
    float _objectRightPos;  // правая граница объекта
    float _screenLeftX; //левая граница экрана в мировых координатах
    float _screenRightX; //правая граница экрана в мировых координатах
    float _objectX;     // Позиция объекта    
    float objectWidth;  // Размер healthBar по X

    void Start()
    {
        _healthBarSpriteRenderer = _healthBarBGNode.GetComponent<SpriteRenderer>();

         objectWidth = _healthBarSpriteRenderer.bounds.size.x;
        



        // Получаем левую границу экрана в мировых координатах
        float cameraZ = Camera.main.transform.position.z;
        Vector3 screenLeftWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -cameraZ));
        _screenLeftX = screenLeftWorld.x;

        // Получаем правую границу экрана в мировых координатах
        // Преобразуем в мировые координаты
        Vector3 screenRightWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.nearClipPlane));
        _screenRightX = screenRightWorld.x;
    }

    // Update is called once per frame
    void Update()
    {
        _objectX = _healthBarBGNode.transform.position.x;
        _objectLeftPos = _objectX - (objectWidth / 2);
        _objectRightPos = _objectX + (objectWidth / 2);

        if (_objectLeftPos <= _screenLeftX || _objectRightPos >= _screenRightX)
        {
            var xPosTemp = transform.localPosition.x;
            transform.localPosition = new Vector2(xPosTemp * -1, transform.localPosition.y);

        }
    }
}
