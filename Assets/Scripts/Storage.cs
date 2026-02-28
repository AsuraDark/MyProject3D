using UnityEngine;
using System;

[RequireComponent(typeof(Base))]
public class Storage : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private int _countResources = 0;

    public event Action<int> ResourceChanged;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    public int GetResource(int countRequiredResources)
    {
        if (_countResources < countRequiredResources)
        {
            return _countResources;
        }
        else
        {
            _countResources -= countRequiredResources;

            ResourceChanged?.Invoke(_countResources);

            return countRequiredResources;
        }
    }

    private void OnEnable()
    {
        _base.ResourceTransfered += AddResource;
    }

    private void OnDisable()
    {
        _base.ResourceTransfered -= AddResource;
    }

    private void AddResource(Resource resource)
    {
        _countResources++;

        ResourceChanged?.Invoke(_countResources);
    }
}