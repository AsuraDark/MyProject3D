using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly int _mouseButton = 0;

    public event Action _leftMouseButtonisClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_mouseButton))
        {
            _leftMouseButtonisClicked?.Invoke();
        }
    }
}