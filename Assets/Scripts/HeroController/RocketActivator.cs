using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RocketActivator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameController _gController;
    private Color _rocketActivatorColor;
    private Image _image;
    private Hero _hero;
    void Start()
    {
        //if (_hero == null)
        //{
        //    _hero = _gController.hero.GetComponent<Hero>();
        //}
        _image = GetComponent<Image>();
        _rocketActivatorColor = _image.color;
    }

    void Update()
    {
        //if (_hero == null && _gController.hero.GetComponent<Hero>() != null)
        //{
        //    _hero = _gController.hero.GetComponent<Hero>();
        //}
        //if (_hero.currentHitPoints >= 0)
        //{
            //if (_hero.hasRocket == true)
            //{
            //    _rocketActivatorColor = new Color(0, 0.5f, 0, 1);
            //    _image.raycastTarget = true;
            //    _image.color = _rocketActivatorColor; // важно!
            //}
            //if (_hero.hasRocket == false)
            //{
            //    _rocketActivatorColor = new Color(0, 0.5f, 0, 0.5f);
            //    _image.raycastTarget = false;
            //    _image.color = _rocketActivatorColor; // важно!
            //}
            //            if (_hero.hasRocket == true)
            if (Player.instance.hasRocket == true)
            {
                _rocketActivatorColor = new Color(0, 0.5f, 0, 1);
                _image.raycastTarget = true;
                _image.color = _rocketActivatorColor; // важно!
            }
            if (Player.instance.hasRocket == false)
            {
                _rocketActivatorColor = new Color(0, 0.5f, 0, 0.5f);
                _image.raycastTarget = false;
                _image.color = _rocketActivatorColor; // важно!
            }
        //}
        //else
        //{
        //    _rocketActivatorColor = new Color(0, 0.5f, 0, 0.5f);
        //    _image.raycastTarget = false;
        //    _image.color = _rocketActivatorColor; // важно!
        //}
    }


    public void OnPointerDown(PointerEventData e)
    {
        //if (_hero.currentHitPoints >= 0)
        //{
            //if (_hero.hasRocket == true)
            if (Player.instance.hasRocket == true)
            {
                _rocketActivatorColor = new Color(0, 0.5f, 0, 0.5f);
                _image.color = _rocketActivatorColor; // важно!
                _image.raycastTarget = false;

                ActivateRocket();
            }
        //}
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    private void ActivateRocket()
    {
        _image.raycastTarget = false;
        //_hero.hasRocket = false;
        Player.instance.hasRocket = false;
        foreach (var enemy in EnemySpawner.enemyesAlive)
        {
            enemy.StartDeathEnemy();
        }
    }
}
