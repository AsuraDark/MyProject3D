using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private TextMeshProUGUI _timerText;

    public void Display()
    {
        _timerText.text = _counter.CurrentCounter.ToString();
    }
}
