using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldActivator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Hero _hero;
    private Color _shieldActivatorColor;
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        _shieldActivatorColor = _image.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hero.currentHitPoints >= 0)
        {
            if (_hero.hasShield == true)
            {
                _shieldActivatorColor = new Color(0, 0.5f, 0, 1);
                _image.raycastTarget = true;
                _image.color = _shieldActivatorColor; // важно!
            }
            if (_hero.hasShield == false)
            {
                _shieldActivatorColor = new Color(0, 0.5f, 0, 0.5f);
                _image.raycastTarget = false;
                _image.color = _shieldActivatorColor; // важно!
            }
            /* это для RocketBaff
            if (_hero.hasRocket == true)
            {
                _shieldActivatorColor = new Color(0, 0.5f, 0, 1);
                _image.raycastTarget = true;
                _image.color = _shieldActivatorColor; // важно!
            }
            if (_hero.hasRocket == false)
            {
                _shieldActivatorColor = new Color(0, 0.5f, 0, 0.5f);
                _image.raycastTarget = false;
                _image.color = _shieldActivatorColor; // важно!
            }
            */
        }
        else
        {
            _shieldActivatorColor = new Color(0, 0.5f, 0, 0.5f);
            _image.raycastTarget = false;
            _image.color = _shieldActivatorColor; // важно!
        }
    }


    public void OnPointerDown(PointerEventData e)
    {
        if (_hero.currentHitPoints >= 0)
        {
            if (_hero.hasShield == true)
            {
                _shieldActivatorColor = new Color(0, 0.5f, 0, 0.5f);
                _image.color = _shieldActivatorColor; // важно!
                _image.raycastTarget = false;

                StartCoroutine(ActivateShield(_hero.shieldDuration));
            }
            /*
            if (_hero.hasRocket == true)
            {
                _shieldActivatorColor = new Color(0, 0.5f, 0, 0.5f);
                _image.color = _shieldActivatorColor; // важно!
                _image.raycastTarget = false;

                //TODO: запуск уничтожения врагов
            }
            */
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    private IEnumerator ActivateShield(float duration)
    {
        _hero.shieldedHeroView.SetActive(true);
        _hero.mainHeroView.SetActive(false);
        _hero.isDestructible = false;

        print("asdad");
        yield return new WaitForSeconds(duration);
        _hero.isDestructible = !_hero.isDestructible;
        _hero.shieldedHeroView.SetActive(false);
        _hero.mainHeroView.SetActive(true);
        _image.raycastTarget = false;
        _hero.hasShield = false;
    }
}
