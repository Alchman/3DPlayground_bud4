using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Teleport : MonoBehaviour
{
    [SerializeField] Teleport otherTeleport;

    private Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    public void EnableCollider(bool enable)
    {
        col.enabled = enable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.DOMove(otherTeleport.transform.position, 1f);
            StartCoroutine(DisableOtherTeleport());
        }
    }

    IEnumerator DisableOtherTeleport()
    {
        otherTeleport.EnableCollider(false);
        yield return new WaitForSeconds(3f);
        otherTeleport.EnableCollider(true);
    }
}
