using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _increasedValue = 1.0f;
    [SerializeField] private float _timeDelay = 0.5f;
    [SerializeField] public UnityEvent CounterIncreased;

    public float CurrentCounter { get; private set; } = 0;

    private Coroutine _coroutine;

    public void StartCounter()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            return;
        }

        _coroutine = StartCoroutine(IncreaseTimer());
    }

    private IEnumerator IncreaseTimer()
    {
        float timer = 0;

        while(enabled)
        {
            timer += Time.deltaTime;

            if(timer >= _timeDelay)
            {
                timer = 0;
                CurrentCounter += _increasedValue;
                CounterIncreased?.Invoke();
            }

            yield return null;
        }
    }
}