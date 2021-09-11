using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : IUpdate
{
    private Vector3 _direction;
    private bool gameStarted;

    public void Update()
    {
        //escape key quits the game
        if (Input.GetKeyDown(KeyCode.Escape))
            GameLoopManager.Instance.Quit();

        if (gameStarted)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                _direction = Vector3.right;
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                _direction = Vector3.left;
            else
                _direction = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.Space))
                GameLoopManager.Instance.BoostClicked();

            GameplayElements.Instance.PlayerShip.GetMovementInput(_direction);
        }
        else if (Input.anyKeyDown)
        {
            GameLoopManager.Instance.StartGame();
            gameStarted = true;
        }
    }
}
