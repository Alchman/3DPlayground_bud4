using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    public UnityEvent OnPressed;
    public UnityEvent OnUnPressed;

    private void OnTriggerEnter(Collider other)
    {
        OnPressed.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnUnPressed.Invoke();
    }
}
