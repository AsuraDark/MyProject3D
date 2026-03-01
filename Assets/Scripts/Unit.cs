using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Unit : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeRotate;
    [SerializeField] private Vector3 _startPosition;

    private WaitForSeconds _waitTravelTime;
    private WaitForSeconds _waitReturn;
    private WaitForSeconds _waitTravelTimeToNewBase;

    private Vector3 _startLocalPosition;
    private Vector3 _startDirection;

    public event Action<Unit, Resource> ResourceTransfered;
    public event Action<Unit> UnitDisabled;

    public void Init()
    {
        _startPosition = transform.position;
        _startLocalPosition = transform.localPosition;
        _startDirection = transform.forward;
    }

    public void TransferResource(Resource resource)
    {
        Sequence sequence = DOTween.Sequence();
        Vector3 resourcePosition = resource.transform.position;
        float _timeTravel = (resourcePosition - _startPosition).magnitude / _speed;

        _waitTravelTime = new WaitForSeconds(_timeRotate + _timeTravel + _timeRotate);
        _waitReturn = new WaitForSeconds(_timeTravel + _timeRotate);
        
        sequence.Append(transform.DOLookAt(resourcePosition, _timeRotate));
        sequence.Append(transform.DOMove(resourcePosition, _timeTravel));
        sequence.Append(transform.DOLookAt(_startPosition, _timeRotate));
        sequence.Append(transform.DOMove(_startPosition, _timeTravel));
        sequence.Append(transform.DOLookAt(_startPosition + _startDirection, _timeRotate));

        StartCoroutine(UpdateResourceInfo(resource));
    }

    private IEnumerator UpdateResourceInfo(Resource resource)
    {
        yield return _waitTravelTime;

        resource.transform.SetParent(transform);

        yield return _waitReturn;

        resource.transform.SetParent(null);

        ResourceTransfered?.Invoke(this, resource);

        resource.Transfer();
    }

    public void CreateNewBase(Flag flag)
    {
        Sequence sequence = DOTween.Sequence();
        Vector3 flagPosition = flag.transform.position;
        float _timeTravel = (flagPosition - _startLocalPosition).magnitude / _speed;

        _waitTravelTimeToNewBase = new WaitForSeconds(_timeRotate + _timeTravel + _timeRotate);

        sequence.Append(transform.DOLookAt(flagPosition, _timeRotate));
        sequence.Append(transform.DOMove(flagPosition, _timeTravel));

        StartCoroutine(CreateBase(_timeTravel, flagPosition, flag));
    }

    private IEnumerator CreateBase(float timeTravel, Vector3 flagPosition, Flag flag)
    {
        yield return _waitTravelTimeToNewBase;

        Base newBase = Instantiate(_basePrefab, flagPosition, _basePrefab.transform.rotation);

        flag.Disable();

        UnitDisabled?.Invoke(this);
    }
}
