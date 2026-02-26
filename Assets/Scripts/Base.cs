using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GiftScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private GiftScanner _giftScanner;
    [SerializeField] private float _countGifts = 0;
    [SerializeField] private List<Unit> _units;

    private List<Gift> _unCollectedGifts = new List<Gift>();
    private List<Gift> _targetGifts = new List<Gift>();

    private void Awake()
    {
        _giftScanner = GetComponent<GiftScanner>();
    }

    private void Start()
    {
        _giftScanner.StartScan();
    }

    private void OnEnable()
    {
        _giftScanner.GiftFound += AddGift;
    }

    private void OnDisable()
    {
        _giftScanner.GiftFound -= AddGift;
    }

    public void TakeGift(Gift gift)
    {
        if (_targetGifts.Contains(gift))
        {
             _targetGifts.Remove(gift);
        }

        _countGifts++;
    }

    private void Update()
    {
        int count = 0;

        for (int i = 0 - count; i < _unCollectedGifts.Count; i++)
        {
            if (_unCollectedGifts[i] == null)
            {
                count++;
                _unCollectedGifts.RemoveAt(i);
            }
        }

        if (_unCollectedGifts.Count == 0)
        {
            return;
        }

        foreach (Gift gift in _unCollectedGifts)
        {
            if(_targetGifts.Contains(gift))
            {
                continue;
            }

            foreach (Unit unit in _units)
            {
                if (unit.IsWorking == false)
                {
                    unit.StartMove(gift.transform.position);
                    _targetGifts.Add(gift);

                    break;
                }
            }
        }

        foreach (Gift gift in _targetGifts)
        {
            if(_unCollectedGifts.Contains(gift))
            {
                _unCollectedGifts.Remove(gift);
            }
        }
    }

    private void AddGift(Gift gift)
    {
        if (_unCollectedGifts.Contains(gift) == false && _targetGifts.Contains(gift) == false)
        {
            _unCollectedGifts.Add(gift);
        }
    }
}
