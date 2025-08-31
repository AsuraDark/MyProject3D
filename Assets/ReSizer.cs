using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSizer : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Update()
    {
        Vector3 newScale = transform.localScale;
        newScale *= _speed;

        transform.localScale += newScale * Time.deltaTime;
    }
}
