using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _increasedValue = 1.0f;
    [SerializeField] private float _timeDelay = 0.5f;

    private Coroutine _coroutine;
    private float CurrentCounter = 0;

    public event Action<float> CounterIncreased;

    private void OnEnable()
    {
        _inputReader.KeyClicked += StartCounter;
    }

    private void OnDisable()
    {
        _inputReader.KeyClicked -= StartCounter;
    }

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
        while (enabled)
        {
            CurrentCounter += _increasedValue;
            CounterIncreased?.Invoke(CurrentCounter);

            yield return new WaitForSeconds(_timeDelay);
        }
    }
}