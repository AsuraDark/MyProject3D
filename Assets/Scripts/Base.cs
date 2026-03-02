using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private int _unitStartValue;
    [SerializeField] private int _unitCreateValue;
    [SerializeField] private Storage _inventory;
    [SerializeField] private FlagPlacer _flagManager;

    private List<Unit> _units = new();

    private bool _createBase = false;
    private int _resourceForCreateUnit = 3;
    private int _resourceForCreateBase = 5;
    private int _countUnits = 0;
    private int _countMaxUnits = 8;
    private Flag _flag;

    public event Action<Resource> ResourceTransfered;

    private void OnEnable()
    {
        _scanner.ResourceFounded += AddResourse;
        _flagManager.FlagCreated += AddResourseForNewBase;
    }

    private void OnDisable()
    {
        _scanner.ResourceFounded -= AddResourse;
        _flagManager.FlagCreated -= AddResourseForNewBase;
    }

    private void Start()
    {
        CreateUnits(_unitStartValue);
    }

    private void Work()
    {
        Unit unit;
        Resource resource;

        if (_createBase == true && _countUnits > 1)
        {
            if (_units.Count > 0 && _inventory.GetResource(_resourceForCreateBase) == _resourceForCreateBase)
            {
                _createBase = false;

                unit = _units.Last();
                _units.Remove(unit);

                unit.CreateNewBase(_flag);
            }
        }

        if (_units.Count > 0 && ResourceProvider.CountResource > 0)
        {
            unit = _units.Last();
            resource = ResourceProvider.GetResource();
            _units.Remove(unit);

            unit.TransferResource(resource);
        }

        if (_createBase == false && _countUnits < _countMaxUnits || _countUnits == 1)
        {
            if (_inventory.GetResource(_resourceForCreateUnit) == _resourceForCreateUnit)
            {
                CreateUnits(_unitCreateValue);
            }
        }
    }

    private void AddResourseForNewBase(Flag flag)
    {
        _createBase = true;

        _flag = flag;

        Work();
    }

    private void AddResourse(Resource resource)
    {
        ResourceProvider.AddResource(resource);

        Work();
    }

    private void CreateUnits(int unitCreateValue)
    {
        for (int i = 0; i < unitCreateValue; i++)
        {
            Unit unit = _unitSpawner.CreateUnit();

            unit.ResourceTransfered += AddUnit;

            unit.UnitDisabled += DisableUnit;

            _units.Add(unit);

            _countUnits++;

            Work();
        }
    }

    private void AddUnit(Unit unit, Resource resource)
    {
        ResourceTransfered?.Invoke(resource);

        _units.Add(unit);

        Work();
    }

    private void DisableUnit(Unit unit)
    {
        unit.ResourceTransfered -= AddUnit;

        unit.UnitDisabled -= DisableUnit;

        _unitSpawner.ReleaseGameObject(unit);

        _countUnits--;

        Work();
    }
}