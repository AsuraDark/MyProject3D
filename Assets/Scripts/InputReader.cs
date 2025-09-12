using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private KeyCode _input = KeyCode.Mouse0;

    public event Action MouseClicked;

    private void Update()
    {
        if (Input.GetKeyDown(_input))
            MouseClicked?.Invoke();
    }
}
