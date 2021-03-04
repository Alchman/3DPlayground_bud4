using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivateUp()
    {
        //StopAllCoroutines();
        //StartCoroutine(GoUp());
        //animator.SetTrigger("GoUp");
        transform.DOMoveY(20, 2f).SetEase(Ease.InElastic);
    }

    public void ActivateDown()
    {
        //StopAllCoroutines();
        //StartCoroutine(GoDown());
        //animator.SetTrigger("GoDown");
        transform.DOMoveY(5, 2f);
    }

    IEnumerator GoUp()
    {
        //движение вверх
        while (transform.position.y < 20)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator GoDown()
    {
        //задержка перед движением вниз
        yield return new WaitForSeconds(1f);

        //движение вниз
        while (transform.position.y > 5)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
            yield return null;
        }
    }
}
