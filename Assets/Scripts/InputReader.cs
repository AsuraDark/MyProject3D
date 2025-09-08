using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    private const KeyCode _leftButtonMouse = KeyCode.Mouse0;

    public event UnityAction KeyClicked;

    private void Update()
    {
        if (Input.GetKeyDown(_leftButtonMouse))
        {
            KeyClicked?.Invoke();
        }
    }
}
