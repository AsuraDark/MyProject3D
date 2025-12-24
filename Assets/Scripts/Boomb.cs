using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DisappearanceTimer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Boomb : MonoBehaviour
{
    [Range(0f,1f)] [SerializeField] private float _recoveryRateAlpha;
    [SerializeField] private float _rangeExplosion;
    [SerializeField] private float _forceExplosion;

    private DisappearanceTimer _disappearanceTimer;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;

    public Action<Boomb> DisapearedBoomb;

    private void Awake()
    {
        _disappearanceTimer = GetComponent<DisappearanceTimer>();
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _disappearanceTimer.StartTimer();
    }

    private void OnEnable()
    {
        _disappearanceTimer.TimerEnded += OnTimerEnded;
    }

    private void OnDisable()
    {
        _disappearanceTimer.TimerEnded -= OnTimerEnded;
    }

    private void Update()
    {
        _meshRenderer.material.color = new Color(_meshRenderer.material.color.r, _meshRenderer.material.color.g, _meshRenderer.material.color.b, _meshRenderer.material.color.a - Time.deltaTime * _recoveryRateAlpha);
    }

    public void ChangePosition(Vector3 position)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.rotation = Quaternion.identity;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.position = position;
    }

    private void OnTimerEnded()
    {
        Explosion();
        DisapearedBoomb?.Invoke(this);
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(_rigidbody.position, _rangeExplosion);
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbodies.Add(rigidbody);
            }
        }

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddExplosionForce(_forceExplosion, _rigidbody.position, _rangeExplosion);
        }
    }
}
