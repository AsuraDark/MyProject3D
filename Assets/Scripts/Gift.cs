using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public event Action<Gift> Disappeared;

    public void Disappear()
    {
        Disappeared.Invoke(this);
    }
}
