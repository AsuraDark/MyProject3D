using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private TextMeshProUGUI _timerText;

    private void OnEnable()
    {
        _counter.ValueIncreased += Display;
    }

    private void OnDisable()
    {
        _counter.ValueIncreased -= Display;
    }

    public void Display(float value)
    {
        _timerText.text = value.ToString();
    }
}
