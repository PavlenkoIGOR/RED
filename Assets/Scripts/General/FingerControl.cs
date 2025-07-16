using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FingerControl : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    float _deltaX, _deltaY;
    Rigidbody2D _rb;
    bool _moveAllowed = false;



    private Collider2D _collider;
    private bool _isHovered = false;

    public RectTransform rectTransform; // Укажите ваш RectTransform

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InstantiateRB(_hero);

    }

    public void InstantiateRB(Hero hero)
    {
        _rb = hero.GetComponent<Rigidbody2D>();
        if (_rb)
        {
            print($"rb {_rb.name}");
        }
    }
    // Update is called once per frame
    void Update()
    {
        //InstantiateRB(_hero);
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

        //    switch (touch.phase)
        //    {
        //        case TouchPhase.Began:
        //            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
        //            {
        //                _deltaX = touchPos.x - transform.position.x;
        //                _deltaY = touchPos.y - transform.position.y;

        //                _moveAllowed = true;

        //                _rb.freezeRotation = true;
        //            }
        //            break;
        //        case TouchPhase.Moved:
        //            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && _moveAllowed)
        //            {
        //                _rb.MovePosition(new Vector2(touchPos.x - _deltaX, touchPos.y - _deltaY));
        //            }
        //            break;
        //        case TouchPhase.Ended:
        //            _moveAllowed = false;
        //            _rb.freezeRotation = true;
        //            break;
        //    }
        //}
        // Проверяем, есть ли курсор(мышь или указатель)




        if (Mouse.current != null)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();

            Vector2 localPoint;

            
            //print($"screenPos {screenPos}");
            //print($"hero {_hero.transform.position}");
            // Преобразуем экранные координаты в локальные для RectTransform
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPos, Camera.main, out localPoint))
            {
                //print($"localPoint {localPoint}");
                // Теперь localPoint — локальные координаты внутри rectTransform
                
                // Можно проверить, внутри ли границ rectTransform
                //if (rectTransform.rect.Contains(localPoint))
                if (_hero._heroCollider.OverlapPoint(screenPos))
                {
                    if (!_isHovered)
                    {
                        _isHovered = true;
                        OnHoverEnter();
                    }
                }
                else
                {
                    if (_isHovered)
                    {
                        _isHovered = false;
                        OnHoverExit();
                    }
                }
            }

        }
    }
    private void OnHoverEnter()
    {

        var go = _hero.transform.Find("GO");
        if (go != null)
        {
            //Debug.Log($"{go.name}");
            go.gameObject.SetActive(true);
        }
        // Ваш код при наведении
    }

    private void OnHoverExit()
    {
        var go = _hero.transform.Find("GO");
        if (go != null)
        {
            //Debug.Log("уход с объекта");
            go.gameObject.SetActive(false);
        }
    }
}
