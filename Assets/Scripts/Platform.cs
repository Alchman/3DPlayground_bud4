using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    [SerializeField] Vector3 moveDistance;
    [SerializeField] float speed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        float moveTime = moveDistance.magnitude / speed;
        transform.DOMove(moveDistance, moveTime)
            .SetRelative(true)
            .SetEase(Ease.Linear)
            .SetUpdate(UpdateType.Fixed)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.DrawSphere(transform.position + moveDistance, 0.2f);
        Gizmos.DrawLine(transform.position, transform.position + moveDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

}
