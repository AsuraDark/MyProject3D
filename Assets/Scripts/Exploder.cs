using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    public void Explosion(List<Rigidbody> rigidbodies, Vector3 center)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddExplosionForce(_explosionForce, center, _explosionRadius);
        }
    }
}
