using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    public UnityEvent OnClickPrimaryThumbstick;

    [SerializeField]
    public InputActionReference toggleReference;

    private void Awake()
    {
        toggleReference.action.started += ctx => Pressed();
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= ctx => Pressed();
    }



    private void Pressed()
    {       
        OnClickPrimaryThumbstick.Invoke();
    }

    
    

}
