using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : IUpdate
{
    Vector3 direction;

    public void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            direction = Vector3.right;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            direction = Vector3.left;
        else
            direction = Vector3.zero;

        GameplayElements.Instance.PlayerShip.GetMovementInput(direction);
    }
}
