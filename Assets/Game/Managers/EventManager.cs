using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public UnityEvent<int> ResourceCurrencyChanged = new UnityEvent<int>();
    public UnityEvent InsufficientResources = new UnityEvent();
}
