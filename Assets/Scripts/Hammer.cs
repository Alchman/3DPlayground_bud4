using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hammer : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 100), 1f).SetRelative(true).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

}
