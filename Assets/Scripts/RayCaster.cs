using System;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    private Ray _ray;
    private RaycastHit _hit;

    public RaycastHit Hit  => _hit;

    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit);
    }
}
