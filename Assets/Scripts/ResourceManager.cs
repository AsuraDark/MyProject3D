using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ResourceManager
{
    private static List<Resource> _freeResources = new();
    private static List<Resource> _busyResources = new();

    public static int CountResource => _freeResources.Count;

    public static void AddResource(Resource resource)
    {
        if(!_freeResources.Contains(resource) && !_busyResources.Contains(resource))
        {
            _freeResources.Add(resource);
        }
    }

    public static Resource GetResource()
    {
        Resource resource = _freeResources.Last();
        _freeResources.Remove(resource);
        _busyResources.Add(resource);

        resource.ResourceTransfered += RemoveResource;

        return resource;
    }

    private static void RemoveResource(Resource resource)
    {
        resource.ResourceTransfered -= RemoveResource;
        _busyResources.Remove(resource);
    }
}
