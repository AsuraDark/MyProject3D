using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime;
    }
}
