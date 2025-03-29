using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Presentation
{
    public class PlayerInputManager : MonoBehaviour
    {
        public char TagChar;
        private float moveVal;
        private float speedUpVal;
        private TetrisManager tetrisManager;

        private void Start()
        {
            tetrisManager = GameObject.FindGameObjectWithTag("Tetris" + TagChar).GetComponent<TetrisManager>();
        }

        // Set moveVal in tetris manager
        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            moveVal = (int)callbackContext.ReadValue<float>();
            tetrisManager.OnMove(moveVal);
        }
        // Set speedUpVal in tetris manager
        public void OnSpeedUp(InputAction.CallbackContext callbackContext)
        {
            speedUpVal = (int)callbackContext.ReadValue<float>();
            tetrisManager.OnSpeedUp(speedUpVal);
        }
        // Invoke restart function
        public void OnReset(InputAction.CallbackContext callbackContext)
        {
            GameObject.FindGameObjectWithTag("GameLogicManager").GetComponent<GameLogicManager>().OnRestart();
        }
    }
}
