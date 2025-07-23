using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    [SerializeField] Hero _hero;
    public RectTransform rectTransform;
    private float canvasWidth;
    float deltaX;
    private Vector3 previousPosition;


    bool isCollide;
    void Start()
    {       
        if (_hero != null)
        {
            canvasWidth = rectTransform.rect.width * rectTransform.lossyScale.x;
            previousPosition = _hero.transform.position;
        }
    }
    void Update()
    {
        if (_hero != null)
        {
            deltaX = _hero.transform.position.x - previousPosition.x;

            #region для смещения канвас при упирании в границы экрана

            if (deltaX < 0 && transform.position.x - rectTransform.rect.width * rectTransform.lossyScale.x / 2 <= GameController.screenLeft
                ||
                deltaX > 0 && transform.position.x + rectTransform.rect.width * rectTransform.lossyScale.x / 2 >= GameController.screenRight)
            {
                isCollide = true;
                transform.position = new Vector3(transform.position.x - deltaX, transform.position.y, transform.position.z);
            }

            #endregion

            if (deltaX>0 && transform.position.x + rectTransform.rect.width * rectTransform.lossyScale.x / 2 <= GameController.screenRight && transform.localPosition.x >= 0
                ||
                deltaX < 0 && transform.position.x - rectTransform.rect.width * rectTransform.lossyScale.x / 2 >= GameController.screenLeft && transform.localPosition.x <= 0)
            {
                transform.position = new Vector3(transform.position.x - deltaX, transform.position.y, transform.position.z);
                if (isCollide == false && deltaX > Mathf.Abs(previousPosition.x - transform.position.x))
                {
                    transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
                }
            }


            previousPosition = _hero.transform.position;
        }






        //if (_hero != null)
        //{
        //    deltaX = _hero.transform.position.x - previousPosition.x;
        //    if (deltaX < 0 && transform.position.x - rectTransform.rect.width * rectTransform.lossyScale.x / 2 <= GameController.screenLeft
        //        ||
        //        deltaX > 0 && transform.position.x + rectTransform.rect.width * rectTransform.lossyScale.x / 2 >= GameController.screenRight)
        //    {
        //        isCollide = true;                
        //    }

        //    //else if (deltaX > 0 && transform.position.x - rectTransform.rect.width * rectTransform.lossyScale.x / 2 >= GameController.screenLeft
        //    //    &&
        //    //        transform.localPosition != new Vector3(0, transform.localPosition.y, transform.localPosition.z)
        //    //        ||
        //    //        deltaX < 0 && transform.position.x + rectTransform.rect.width * rectTransform.lossyScale.x / 2 <= GameController.screenRight
        //    //        &&
        //    //        transform.localPosition != new Vector3(0, transform.localPosition.y, transform.localPosition.z))
        //    //{
        //    //    isCollide=false;
        //    //}
        //    //if (isCollide == false && deltaX > 0)
        //    //{
        //    //    if (deltaX > previousPosition.x - transform.position.x)
        //    //    {
        //    //        transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        //    //        isCollide = false;
        //    //    }
        //    //    else
        //    //    {
        //    //        transform.position = new Vector3(transform.position.x - deltaX, transform.position.y, transform.position.z);
        //    //    }
        //    //}
        //    //else if (isCollide == false && deltaX < 0)
        //    //{
        //    //    if (deltaX > previousPosition.x - transform.position.x)
        //    //    {
        //    //        transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        //    //        isCollide = false;
        //    //    }
        //    //    else
        //    //    {
        //    //        transform.position = new Vector3(transform.position.x - deltaX, transform.position.y, transform.position.z);
        //    //    }
        //    //}





        //    //if (isCollide == false && transform.position.x - rectTransform.rect.width * rectTransform.lossyScale.x / 2 >= GameController.screenLeft && transform.position.x != _hero.transform.position.x)
        //    //{
        //    //    transform.localPosition = new Vector3(transform.localPosition.x - deltaX, transform.localPosition.y, transform.localPosition.z);
        //    //    if (deltaX > previousPosition.x - transform.position.x)
        //    //    {
        //    //        transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        //    //        isCollide = false;
        //    //    }
        //    //}
        //    if (isCollide)
        //    {
        //        transform.position = new Vector3(transform.position.x - deltaX, transform.position.y, transform.position.z);
        //    }



        //        previousPosition = _hero.transform.position;
        //}
    }


}
