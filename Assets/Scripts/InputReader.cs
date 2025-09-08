using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    public event UnityAction KeyClicked;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            KeyClicked?.Invoke();
        }
    }
}
