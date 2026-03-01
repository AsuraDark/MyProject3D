using UnityEngine;
using System;

public class Resource : MonoBehaviour
{
    public event Action<Resource> ResourceTransfered;

    public void Transfer()
    {
        ResourceTransfered?.Invoke(this);
    }
}