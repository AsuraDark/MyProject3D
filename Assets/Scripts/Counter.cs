using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _increasedValue;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _timerText;

    private Coroutine _coroutine;

    private bool _isButtonClicked = false;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _isButtonClicked = !_isButtonClicked;

        if (_isButtonClicked )
        {
            _coroutine = StartCoroutine(IncreaseTimer());
        }
        else
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator IncreaseTimer()
    {
        float timer = 0;

        while(true)
        {
            timer = Convert.ToSingle(_timerText.text);
            timer += _increasedValue;
            _timerText.text = timer.ToString("");
            yield return null;
        }
    }
}