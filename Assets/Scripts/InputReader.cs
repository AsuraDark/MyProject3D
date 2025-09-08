using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private Counter _counter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _counter.StartCounter();
        }
    }
}
