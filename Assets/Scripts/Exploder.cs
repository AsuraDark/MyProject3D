using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Explosion(List<Cube> cubes, Cube explodingCube)
    {
        Vector3 explosionCenter = explodingCube.transform.position;

        foreach (Cube cube in cubes)
        {
            if(cube.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius);
        }
    }

    public void Explosion(Cube explodingCube)
    {
        Vector3 explosionCenter = explodingCube.transform.position;
        float scale = explodingCube.transform.localScale.x;

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
