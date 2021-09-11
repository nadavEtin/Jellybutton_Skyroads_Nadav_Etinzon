using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private float _regDistance = 1.9f;
    [SerializeField] private float _boostDistance = 1.3f;
    [SerializeField] private float _regHeight = 0.45f;
    [SerializeField] private float _boostHeight = 0.4f;
    [SerializeField] private float _heightDamping = 2.0f;
    [SerializeField] private float _distanceDamping = 2.0f;
    [SerializeField] private float _rotationDamping = 3.0f;
    [SerializeField] private Transform _target;

    private float _curDistance, _curHeight;

    private void Start()
    {
        EventBus.Instance.Subscribe(GameplayEventType.BoostClicked, BoostZoom);
        _curDistance = _regDistance;
        _curHeight = _regHeight;
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(GameplayEventType.BoostClicked, BoostZoom);
    }

    private void BoostZoom(BaseEventParams par)
    {
        var boostParam = (BoostParams)par;
        if(boostParam.BoostIsOn)
        {
            _curDistance = _boostDistance;
            _curHeight = _boostHeight;
        }
        else
        {
            _curDistance = _regDistance;
            _curHeight = _regHeight;
        }
    }

    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!_target)
        {
            return;
        }

        // Calculate the current rotation angles
        float wantedRotationAngle = _target.eulerAngles.y;
        float wantedHeight = _target.position.y + _curHeight;
        float wantedDistance = _target.position.z - _curDistance;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;
        float currentDistance = transform.position.z;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

        currentDistance = Mathf.Lerp(currentDistance, wantedDistance, _distanceDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = _target.position - currentRotation * Vector3.forward * _curDistance;
        pos.y = currentHeight;
        pos.z = currentDistance;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(_target);
    }
}