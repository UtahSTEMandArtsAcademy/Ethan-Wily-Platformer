using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spikeys : MonoBehaviour
{
    public UnityEvent enter;
    private void OnTriggerEnter(Collider other)
    {
        enter.Invoke();
    }
}
