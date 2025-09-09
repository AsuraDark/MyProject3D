using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    private const KeyCode _input = KeyCode.Mouse0;

    public event UnityAction KeyClicked;

    private void Update()
    {
        if (Input.GetKeyDown(_input))
        {
            KeyClicked?.Invoke();
        }
    }
}
