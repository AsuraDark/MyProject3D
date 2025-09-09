using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private TextMeshProUGUI _timerText;

    private void OnEnable()
    {
        _counter.CounterIncreased += Display;
    }

    private void OnDisable()
    {
        _counter.CounterIncreased -= Display;
    }

    public void Display(float counter)
    {
        _timerText.text = counter.ToString();
    }
}
