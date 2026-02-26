using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GiftScanner : MonoBehaviour
{
    [SerializeField] private float _scanDelay;
    [SerializeField] private float _scanRange;
    [SerializeField] private Vector3 _scanCenter;

    public event Action<Gift> GiftFound;

    private Coroutine _coroutine;

    public void StartScan()
    {
        StopScan();
        _coroutine = StartCoroutine(Scan());
    }

    public void StopScan()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator Scan()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_scanDelay);
        List<Collider> colliders;

        while (enabled)
        {
            colliders = Physics.OverlapSphere(_scanCenter, _scanRange).ToList();

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Gift gift))
                {
                    GiftFound?.Invoke(gift);
                }
            }

            yield return waitingTime;
        }
    }
}
