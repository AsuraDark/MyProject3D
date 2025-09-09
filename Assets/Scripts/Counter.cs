using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _increasedValue = 1.0f;
    [SerializeField] private float _timeDelay = 0.5f;

    private Coroutine _coroutine;
    private float _currentValue = 0;

    public event Action<float> ValueIncreased;

    private void OnEnable()
    {
        _inputReader.KeyClicked += StartWork;
    }

    private void OnDisable()
    {
        _inputReader.KeyClicked -= StartWork;
    }

    public void StartWork()
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
        WaitForSeconds waitTime = new(_timeDelay);

        while (enabled)
        {
            _currentValue += _increasedValue;
            ValueIncreased?.Invoke(_currentValue);

            yield return waitTime;
        }
    }
}