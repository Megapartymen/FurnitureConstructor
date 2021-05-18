using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCameraMoveInLimit : MonoBehaviour
{
    [SerializeField] private Transform _targetRotationVertical;
    [SerializeField] private Transform _targetRotationHorizontal;
    
    private float _cameraPositionY;
    private float _cameraPositionX;

    private float _maxLimitY;
    private float _minLimitY;
    private float _rightLimitX;
    private float _leftLimitX;

    private bool _rotationPermission;

    private void Start()
    {
        _cameraPositionY = 0f;
        _cameraPositionX = 0f;

        _maxLimitY = 20f;
        _minLimitY = -10f;

        _rightLimitX = 20f;
        _leftLimitX = -20f;

        _rotationPermission = false;
    }

    private void Update()
    {
        MoveCameraInLimit(CheckRotationPermission());
    }
    
    private void MoveCameraInLimit(bool rotationPermission)
    {
        if (rotationPermission)
        {
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");

            _cameraPositionX += mouseX;
            _cameraPositionY += mouseY;

            if (_cameraPositionY >= _minLimitY && _cameraPositionY <= _maxLimitY)
                _targetRotationHorizontal.Rotate(-mouseY, 0, 0, Space.Self);

            if (_cameraPositionX >= _leftLimitX && _cameraPositionX <= _rightLimitX)
                _targetRotationVertical.Rotate(0, mouseX, 0, Space.World);
        }
        else
        {
            if (_cameraPositionY > _maxLimitY)
                _cameraPositionY = _maxLimitY;

            if (_cameraPositionY < _minLimitY)
                _cameraPositionY = _minLimitY;

            if (_cameraPositionX > _rightLimitX)
                _cameraPositionX = _rightLimitX;

            if (_cameraPositionX < _leftLimitX)
                _cameraPositionX = _leftLimitX;
        }
    }

    private bool CheckRotationPermission()
    {
        if (Input.GetMouseButtonDown(1))
            _rotationPermission = true;

        if (Input.GetMouseButtonUp(1))
            _rotationPermission = false;

        return _rotationPermission;
    }
}
