using UnityEngine;

[RequireComponent(typeof(AlarmSystem))]
public class House : MonoBehaviour
{
    [SerializeField] private ThiefDetector _thiefDetector;

    private AlarmSystem _alarmSystem;

    private void Awake()
    {
        _alarmSystem = GetComponent<AlarmSystem>();
    }

    private void OnEnable()
    {
        _thiefDetector.ThiefEntered += _alarmSystem.StartAlarm;
        _thiefDetector.ThiefExited += _alarmSystem.StopAlarm;
    }

    private void OnDisable()
    {
        _thiefDetector.ThiefEntered -= _alarmSystem.StartAlarm;
        _thiefDetector.ThiefExited -= _alarmSystem.StopAlarm;
    }
}
