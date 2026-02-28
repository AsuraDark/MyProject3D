using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private FlagManager _flagManager;

    public event Action BaseBuilding;

    private void FixedUpdate()
    {
        if (_inputReader.GetClickSetFlag())
        {
            BaseBuilding?.Invoke();
        }
    }
}