using UnityEngine;

public class HealthBarMove : MonoBehaviour
{
    Vector3 LeftScreenBorder;
    [SerializeField] private GameObject _healthBarBGNode;
    SpriteRenderer _healthBarSpriteRenderer;


    float _objectLeftPos;  // ����� ������� �������
    float _objectRightPos;  // ������ ������� �������
    float _screenLeftX; //����� ������� ������ � ������� �����������
    float _screenRightX; //������ ������� ������ � ������� �����������
    float _objectX;     // ������� �������    
    float objectWidth;  // ������ healthBar �� X

    void Start()
    {
        _healthBarSpriteRenderer = _healthBarBGNode.GetComponent<SpriteRenderer>();

         objectWidth = _healthBarSpriteRenderer.bounds.size.x;
        



        // �������� ����� ������� ������ � ������� �����������
        float cameraZ = Camera.main.transform.position.z;
        Vector3 screenLeftWorld = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -cameraZ));
        _screenLeftX = screenLeftWorld.x;

        // �������� ������ ������� ������ � ������� �����������
        // ����������� � ������� ����������
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
