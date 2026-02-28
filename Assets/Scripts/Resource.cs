using UnityEngine;
using System;

public class Resource : MonoBehaviour
{
    public event Action<Resource> ResourceTransfered;

    public bool IsFound { get; private set; }

    public void Find()
    {
        if (!IsFound)
        {
            IsFound = true;
        }
    }

    public void Transfer()
    {
        ResourceTransfered?.Invoke(this);
    }

    private void OnEnable()
    {
        IsFound = false;
    }
}