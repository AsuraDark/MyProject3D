using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private BaseDetector _baseDetector;
    [SerializeField] private GiftDetector _giftDetector;
    [SerializeField] private Transform _container;

    private Vector3 _startPosition;

    private Gift _gift;

    private Coroutine _coroutine;

    public bool IsWorking { get; private set; } = false;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Start()
    {
        _giftDetector.gameObject.SetActive(false);
        _baseDetector.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _giftDetector.GiftDetected += TakeGift;
        _baseDetector.BaseDetected += TransportGift;
    }

    private void OnDisable()
    {
        _giftDetector.GiftDetected -= TakeGift;
        _baseDetector.BaseDetected -= TransportGift;
    }

    public void StartMove(Vector3 targetPosition)
    {
        StopMove();
        _coroutine = StartCoroutine(Move(targetPosition));
    }

    public void StopMove()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        IsWorking = true;

        Vector3 direction;

        while(Math.Round(transform.position.x, 1) != Math.Round(targetPosition.x, 1))
        {
            direction = targetPosition - transform.position;
            transform.Translate(direction.normalized * _speed * Time.deltaTime);

            if(Math.Round(transform.position.x, 1) == Math.Round(targetPosition.x, 1))
            {
                _giftDetector.gameObject.SetActive(true);
            }

            yield return null;
        }

        while (Math.Round(transform.position.x, 1) != Math.Round(_startPosition.x, 1))
        {
            direction = _startPosition - transform.position;
            transform.Translate(direction.normalized * _speed * Time.deltaTime);

            if (Math.Round(transform.position.x, 1) == Math.Round(_startPosition.x, 1))
            {
                _baseDetector.gameObject.SetActive(true);
            }
            yield return null;
        }

        IsWorking = false;
    }

    private void TakeGift(Gift gift)
    {
        _giftDetector.gameObject.SetActive(false);

        gift.transform.position = _container.position;
        gift.transform.SetParent(_container);

        _gift = gift;
    }

    private void TransportGift(Base @base)
    {
        _baseDetector.gameObject.SetActive(false);

        @base.TakeGift(_gift);
    }
}
