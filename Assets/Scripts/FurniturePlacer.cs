using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePlacer : MonoBehaviour
{
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private GameObject _rightWallAngleHorizontal;
    [SerializeField] private GameObject _rightWallAngleVertical;
    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _leftWallAngleHorizontal;
    [SerializeField] private GameObject _leftWallAngleVertical;

    private Furniture _flyingFurniture;
    private Camera _mainCamera;
    private int _raycastChekDistance;

    private void Start()
    {
        _mainCamera = Camera.main;
        _raycastChekDistance = 100;
    }

    private void Update()
    {
        if (_flyingFurniture != null)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            ChoisePlacePosition(_floor, ray);
            ChoisePlacePosition(_rightWall, ray, true);
            ChoisePlacePosition(_leftWall, ray, true);
            TryMagnetToAngle(_rightWallAngleHorizontal, ray);
            TryMagnetToAngle(_rightWallAngleVertical, ray);
            TryMagnetToAngle(_leftWallAngleHorizontal, ray);
            TryMagnetToAngle(_leftWallAngleVertical, ray);
            TryPlaceFurniture();
        }
    }

    private void TryPlaceFurniture()
    {
        if (Input.GetMouseButton(0))
        {
            _flyingFurniture = null;
        }
    }
    
    private void ChoisePlacePosition(GameObject plane, Ray ray, bool wall)
    {
        if (plane.GetComponent<MeshCollider>().Raycast(ray, out RaycastHit hit, _raycastChekDistance))
        {
            _flyingFurniture.transform.position = PivotCorrection(ray.GetPoint(hit.distance));
            _flyingFurniture.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
    }

    private void ChoisePlacePosition(GameObject plane, Ray ray)
    {
        if (plane.GetComponent<MeshCollider>().Raycast(ray, out RaycastHit hit, _raycastChekDistance))
        {
            _flyingFurniture.transform.position = PivotCorrection(ray.GetPoint(hit.distance));
        }
    }

    private void TryMagnetToAngle(GameObject angleMagnet, Ray ray)
    {
        if (angleMagnet.GetComponent<MeshCollider>().Raycast(ray, out RaycastHit hit, _raycastChekDistance))
        {

        }
    }

    private void TryMagnetToFurniture(Ray ray)
    {
        if (true)
        {

        }
    }


    private Vector3 PivotCorrection(Vector3 pivotPosition)
    {
        Vector3 pivotCorrection = _flyingFurniture.transform.localScale / 2;

        pivotPosition.x += pivotCorrection.x;
        pivotPosition.y += pivotCorrection.y;
        pivotPosition.z -= pivotCorrection.z;

        return pivotPosition;
    }

    public void ToStartPlacingFurniture(Furniture furniture)
    {
        if (_flyingFurniture != null)
            Destroy(_flyingFurniture.gameObject);

        _flyingFurniture = Instantiate(furniture);
    }
}
