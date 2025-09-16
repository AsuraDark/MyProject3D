using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DisappearanceTimer : MonoBehaviour
{
    [SerializeField] private float _minTimeDelay;
    [SerializeField] private float _maxTimeDelay;

    public event Action TimerEnded;

    public void StartTimer()
    {
        StartCoroutine(WaitCooldown());
    }

    private IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(Random.Range(_minTimeDelay, _maxTimeDelay));

        TimerEnded?.Invoke();
    }
}
