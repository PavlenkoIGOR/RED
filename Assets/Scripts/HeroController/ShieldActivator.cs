using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldActivator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameController _gController;
    private Color _shieldActivatorColor;
    private Image _image;
    private Hero _hero;

    void Start()
    {
        if (_hero == null)
        {
            _hero = _gController.hero.GetComponent<Hero>();
        }
        _image = GetComponent<Image>();
        _shieldActivatorColor = _image.color;
    }

    void Update()
    {
        if (_hero == null)
        {
            _hero = _gController.hero.GetComponent<Hero>();
        }
        if (_shieldActivatorColor != null)
        {
            if (_hero.currentHitPoints >= 0)
            {
                if (Player.instance.hasShield == true)
                {
                    _shieldActivatorColor = new Color(0, 0.5f, 0, 1);
                    _image.raycastTarget = true;
                    _image.color = _shieldActivatorColor; // важно!
                }
                if (Player.instance.hasShield == false)
                {
                    _shieldActivatorColor = new Color(0, 0.5f, 0, 0.5f);
                    _image.raycastTarget = false;
                    _image.color = _shieldActivatorColor; // важно!
                }
            }
            else
            {
                _shieldActivatorColor = new Color(0, 0.5f, 0, 0.5f);
                _image.raycastTarget = false;
                _image.color = _shieldActivatorColor; // важно!
            }
        }
    }


    public void OnPointerDown(PointerEventData e)
    {
        if (_hero.currentHitPoints >= 0)
        {
            if (Player.instance.hasShield == true)
            {
                _shieldActivatorColor = new Color(0, 0.5f, 0, 0.5f);
                _image.color = _shieldActivatorColor; // важно!
                _image.raycastTarget = false;

                StartCoroutine(ActivateShield(_hero.shieldDuration));
            }
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

        yield return new WaitForSeconds(duration);
        _hero.isDestructible = !_hero.isDestructible;
        _hero.shieldedHeroView.SetActive(false);
        _hero.mainHeroView.SetActive(true);
        _image.raycastTarget = false;
        Player.instance.hasShield = false;
    }


}
