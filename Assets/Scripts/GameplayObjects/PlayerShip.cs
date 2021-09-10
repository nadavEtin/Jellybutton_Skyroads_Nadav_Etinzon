using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IPlayer
{
    #region Editor Fields

    [Range(0f, 90f)]
    [SerializeField] private int _maxShipRotationAngle = 45;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private Transform _rotatingTransform, _movementTransform;
    [SerializeField] private float _moveSpeed = 10;

    #endregion

    #region Private Fields

    private float _angleFactor;
    private float _angleFactorIncrement;
    private float _leftRoadEdge, _rightRoadEdge;
    private Vector3 _movementDir;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        EventBus.Instance.Subscribe(GameplayEventType.StartGame, StartGame);
    }

    private void FixedUpdate()
    {
        //TODO: start moving only after the game is finished setting up, send an event with the road width
        MovePlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("boom");
    }

    #endregion

    #region Methods

    private void StartGame(BaseEventParams par)
    {
        var roadWidth = GameplayElements.Instance.RoadPieceSize;
        _leftRoadEdge = roadWidth.min.x * 0.9f;
        _rightRoadEdge = roadWidth.max.x * 0.9f;
    }

    public void GetMovementInput(Vector3 direction)
    {
        _movementDir = direction;
    }

    private void MovePlayer()
    {
        //move player ship and prevent it from leaving road boundaries
        var xMovement = Mathf.Clamp(_movementTransform.localPosition.x + (_movementDir * _moveSpeed * Time.fixedDeltaTime).x, _leftRoadEdge, _rightRoadEdge);
        _movementTransform.localPosition = new Vector3(xMovement, _movementTransform.localPosition.y, _movementTransform.localPosition.z);

        //rotate the ship towards movement direction or to the reset position
        _angleFactorIncrement = (_movementDir.x * _rotationSpeed);
        if (_movementDir != Vector3.zero)
            ShipRotationTurn();
        else if (_rotatingTransform.localRotation.z != 0)
            ShipRotationReset();
    }

    private void ShipRotationReset()
    {
        _rotatingTransform.localRotation = Quaternion.Slerp(_rotatingTransform.localRotation, Quaternion.Euler(0, 0, 0), _rotationSpeed * Time.deltaTime);
        if (_rotatingTransform.localRotation.z < 0.1f)
            _angleFactor = 0;
    }

    private void ShipRotationTurn()
    {
        _angleFactor = Mathf.Clamp(_angleFactor + _angleFactorIncrement * Time.deltaTime, -1, 1);
        var angleInDegrees = _maxShipRotationAngle * _angleFactor * -1;
        _rotatingTransform.localRotation = Quaternion.AngleAxis(angleInDegrees, transform.forward);
    }

    #endregion
}
