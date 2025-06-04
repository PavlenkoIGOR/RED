using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField] private Transform point;
    [SerializeField] private float speedToPos;

    public void Setposition(Enemy enm)
    {
        StartCoroutine(enmToPos(enm));
    }

    public IEnumerator enmToPos(Enemy enm)
    {
        while (enm.transform.position != point.position)
        {
          
            enm.transform.position = Vector2.MoveTowards(enm.transform.position, point.position, speedToPos * Time.deltaTime);
          
            yield return null;

        }
      
        enm.ActivateShoot();
    }
}
