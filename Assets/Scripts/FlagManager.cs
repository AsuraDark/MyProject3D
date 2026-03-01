using System;
using UnityEditor;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField] private FlagPreviewer _flagPreviewer;
    [SerializeField] private Flag _flag;
    [SerializeField] private Transform _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _canCreatePreviewFlag = false;
    [SerializeField] private bool _canCreateFlag = false;

    private RaycastHit _hitInfo;

    public event Action<Flag> FlagCreated;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        CreatePreviewFlag(_canCreatePreviewFlag);
    }

    public void ChangePosibilityCreateFlag()
    {
        _canCreatePreviewFlag = !_canCreatePreviewFlag;
        _canCreateFlag = !_canCreateFlag;
    }

    private void CreatePreviewFlag(bool canDrawPrevieFlag)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Vector3 direction = (mousePos - _camera.position).normalized;

        if (Physics.Raycast(_camera.position, direction, out _hitInfo, float.MaxValue, _layerMask) && canDrawPrevieFlag)
        {
            if (!_flagPreviewer.IsActive)
            {
                _flagPreviewer.Enable();
            }

            _flagPreviewer.SetPosition(_hitInfo.point);
        }
        else
        {
            _flagPreviewer.Disable();
        }
    }

    public void CreateFlag()
    {
        if (_hitInfo.point != null && _canCreateFlag && _canCreatePreviewFlag)
        {
            if (!_flag.IsActive)
            {
                _flag.Enable();
            }

            _flag.SetPosition(_hitInfo.point);

            _canCreatePreviewFlag = false;

            _canCreateFlag = false;

            FlagCreated?.Invoke(_flag);
        }
    }
}