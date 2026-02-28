using System;
using System.Collections;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scanDelay;
    [SerializeField] private float _scanRange;
    [SerializeField] private LayerMask _resourceLayerMask;

    public event Action<Resource> ResourceFounded;

    private void Start()
    {
        StartCoroutine(StartScan());
    }

    private IEnumerator StartScan()
    {
        WaitForSeconds wait;

        while (enabled)
        {
            wait = new WaitForSeconds(_scanDelay);

            yield return wait;

            ScanResources();
        }
    }

    private void ScanResources()
    {
        Collider[] _resourceColliders = Physics.OverlapSphere(transform.position, _scanRange, _resourceLayerMask);

        foreach (var item in _resourceColliders)
        {
            if (item.TryGetComponent(out Resource resource) && !resource.IsFound)
            {
                resource.Find();

                ResourceFounded?.Invoke(resource);
            }
        }
    }
}