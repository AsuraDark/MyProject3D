using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AlarmSystem))]
public class SoundVolumeChanger : MonoBehaviour
{
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _volumeChangeValue = 0.001f;
    [SerializeField] private float _timeDelay = 0.1f;

    private AlarmSystem _alarmSystem;
    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _alarmSystem = GetComponent<AlarmSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _alarmSystem.AlarmEnabled += IncreaseChangeVolume;
        _alarmSystem.AlarmDisabled += DecreaseChangeVolume;
    }

    private void OnDisable()
    {
        _alarmSystem.AlarmEnabled -= IncreaseChangeVolume;
        _alarmSystem.AlarmDisabled -= DecreaseChangeVolume;
    }

    private void Update()
    {
        if (_audioSource.volume >= _maxVolume)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        if (_audioSource.volume <= _minVolume)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _audioSource.Stop();
            }
        }
    }

    private IEnumerator ChangeVolume(float delay, float targetValue)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetValue, _volumeChangeValue);

            yield return wait;
        }
    }

    private void IncreaseChangeVolume()
    {
        if(_audioSource.isPlaying != true)
        {
            _audioSource.Play();
        }

        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeVolume(_timeDelay, _maxVolume));
    }

    private void DecreaseChangeVolume()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeVolume(_timeDelay, _minVolume));
    }
}
