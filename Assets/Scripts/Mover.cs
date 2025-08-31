using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime;
    }
}
