using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Explosion(List<Rigidbody> rigidbodies, Cube cube)
    {
        Vector3 explosionCenter = cube.transform.position;

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius);
        }
    }

    public void Explosion(Cube cube)
    {
        Vector3 explosionCenter = cube.transform.position;
        float scale = cube.transform.localScale.x;

        float explosionRadius = _explosionRadius / scale;
        float explosionForce = _explosionForce / scale; 

        Collider[] colliders = Physics.OverlapSphere(explosionCenter, explosionRadius);
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbodies.Add(rigidbody);
            }
        }

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddExplosionForce(explosionForce, explosionCenter, explosionRadius);
        }
    }
}
