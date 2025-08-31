using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverToView : MonoBehaviour
{
    [SerializeField] private GameObject _view;

    private void Start()
    {
        transform.LookAt( _view.transform );
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime;
    }
}
